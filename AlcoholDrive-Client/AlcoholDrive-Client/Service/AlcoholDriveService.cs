using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Model.Response;
using AlcoholDrive_Client.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlcoholDrive_Client.Service {
    public class AlcoholDriveService {

        private AlcDriveState _state;
        private readonly AlcoholDriveRepository repository;
        private readonly MessageDeliveryService deliveryService;

        /// <summary>
        /// 飲酒運転となる呼気中アルコール濃度の基準値 mg/L
        /// </summary>
        public const double BAC_LIMIT = 0.15;

        /// <summary>
        /// キャリブレーションの値
        /// </summary>
        private double R0;

        /// <summary>
        /// true:接続, false:切断
        /// </summary>
        public bool IsConnect {
            get {
                return repository.IsConnect;
            }
        }

        public AlcoholDriveService(AlcoholDriveRepository repository,
            MessageDeliveryService deliveryService) : base() {

            this.R0 = -1;
            this._state = AlcDriveState.NONE;
            this.repository = repository;
            this.deliveryService = deliveryService;

            this.deliveryService.MessageSubject.Subscribe(message => {
                int cmd = message.Item1;

                //接続
                if (cmd == DeviceCommands.CONNECT_DEVICE) {
                    ConnectDrive();
                    this.R0 = Calibration();
                    AlcLogService.Write($"R0 = {this.R0}");
                }
                //切断
                if (cmd == DeviceCommands.DISCONNECT_DEVICE) {
                    DisconnectDrive();
                }
                //デバイス接続確認
                if (cmd == DeviceCommands.IS_CONNECT_DEVICE) {
                    deliveryService.PostCommand(DeviceCommands.IS_CONNECT_DEVICE_RES, IsConnect);
                }

                //スキャン開始 結果送信
                if (cmd == AlcoholDriveFrontCommands.START_SCANNING) {
                    StartScanning();
                }

                //スキャン停止
                if (cmd == AlcoholDriveFrontCommands.STOP_SCANNING) {
                    StopScanning();
                }
            });
        }

        /// <summary>
        /// デバイスに接続する
        /// </summary>
        /// <returns></returns>
        public bool ConnectDrive() {
            try {
                this._state = AlcDriveState.CONNECTED;
                return repository.ConnectDrive();
            } catch (Exception ex) {
                this._state = AlcDriveState.FAIL;
                deliveryService.PostException(ex);
                return false;
            }
        }

        /// <summary>
        /// デバイスとの接続を切断する
        /// </summary>
        /// <returns></returns>
        public bool DisconnectDrive() {
            try {
                this._state = AlcDriveState.DISCONNECT;
                return repository.DisconnectDrive();
            } catch (Exception ex) {
                this._state = AlcDriveState.FAIL;
                deliveryService.PostException(ex);
                return false;
            }
        }

        /// <summary>
        /// アルコール検知を開始する
        /// </summary>
        public void StartScanning() {
            try {
                //スキャン開始
                repository.StartScanning();
                AlcDriveResult alcDriveResult = new AlcDriveResult();
                alcDriveResult.State = AlcDriveState.SCANNING;
                string json = JsonConvert.SerializeObject(alcDriveResult);
                this.deliveryService.PostCommand(AlcoholDriveFrontCommands.SCAN_RESULT, json);


                //結果送信                
                alcDriveResult = CheckAlcohol(this.R0);
                this._state = AlcDriveState.OK;
                alcDriveResult.State = this._state;
                json = JsonConvert.SerializeObject(alcDriveResult);
                this.deliveryService.PostCommand(AlcoholDriveFrontCommands.SCAN_RESULT, json);
            } catch (Exception ex) {
                this._state = AlcDriveState.FAIL;
                deliveryService.PostException(ex);
            }
        }

        /// <summary>
        /// アルコール検知を停止する
        /// </summary>
        public void StopScanning() {
            try {
                repository.StopScanning();
                this._state = AlcDriveState.CONNECTED;
            } catch (Exception ex) {
                this._state = AlcDriveState.FAIL;
                deliveryService.PostException(ex);
            }
        }

        /// <summary>
        /// BAC取得のためのR0を取得する
        /// </summary>
        /// <returns></returns>
        private double Calibration() {
            const int READING_COUNT = 4;

            List<ushort> values = new List<ushort>();
            //データ受信を5回行う 32 * 4 = 128
            for (int i = 0; i < READING_COUNT; i++) {
                values.AddRange(this.repository.ReadingAlcoholValue());
            }


            //キャリブレーション
            double sensorVolt = 0;
            double RS = 0; // 空気中のRS値
            double R0 = 0; // アルコール中のR0値
            double avgValue = 0;

            //平均値の算出 100回
            values.Take(100).ToList().ForEach(v => { avgValue += v; });
            avgValue /= 100;

            sensorVolt = (avgValue / 1024) * 5.0;
            RS = (5.0 - sensorVolt) / sensorVolt; // 省略 RL
            R0 = RS / 60.0; // きれいな空気中のRS/R0=60

            return R0;
        }

        /// <summary>
        /// 血中酸素濃度を取得 [mg/L]
        /// </summary>
        /// <param name="R0">キャリブレーションした値</param>
        /// <returns></returns>
        public AlcDriveResult CheckAlcohol(double R0) {
            if (R0 == -1) {
                throw new Exception("キャリブレーションが実施されていません");
            }

            List<ushort> values = this.repository.ReadingAlcoholValue();

            ushort sensorValue = values.LastOrDefault();
            double sensorVolt = (double)sensorValue / 1024 * 5.0;
            // 測定対象のガス中のRS値
            double RS_gas = (5.0 - sensorVolt) / sensorVolt; // 省略 *RL

            // RS_gas/RS_air 空気中のRS割合
            double ratio = RS_gas / R0;  // ratio = RS/R0   
            double BAC = Math.Pow(10, -1 * (((Math.Log10(ratio)) + 0.2391) / 0.6008));  // mg/L中のBAC

            //Serial.println(BAC * 2);  // 血液内のアルコール濃度[mg/ml]に変換(1:2)

            AlcLogService.Write($"BAC = {BAC}[mg/L]");

            return new AlcDriveResult() {
                BAC = BAC,
                State = AlcDriveState.OK,
                DrivableResult = BAC < BAC_LIMIT
            };
        }

    }
}
