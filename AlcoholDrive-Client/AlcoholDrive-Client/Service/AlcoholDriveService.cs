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


        private const int R0_CLEAN_AIR_FATOR = 60;

        private const int CALIBRATION_SAMPLE_COUNTS = 50;

        private const int READ_SAMPLE_TIMES = 50;

        private const int RL_VALUE = 200;

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

        private double ReadData(int sampleCounts) {
            const int READING_COUNT = 4;
            List<ushort> values = new List<ushort>();
            //データ受信を5回行う 32 * 4 = 128
            for (int i = 0; i < READING_COUNT; i++) {
                values.AddRange(this.repository.ReadingAlcoholValue());
            }

            double avgValue = 0;
            //平均値の算出
            values.Take(sampleCounts).ToList().ForEach(v => {
                avgValue += (RL_VALUE * (1024 - v) / (double)(v));
            });
            avgValue /= sampleCounts;

            return avgValue;
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

            double BAC = GetBAC();

            //Serial.println(BAC * 2);  // 血液内のアルコール濃度[mg/ml]に変換(1:2)

            AlcLogService.Write($"BAC = {BAC}[mg/L]");

            return new AlcDriveResult() {
                BAC = BAC,
                State = AlcDriveState.OK,
                DrivableResult = BAC < BAC_LIMIT
            };
        }

        /// <summary>
        /// BAC取得のためのR0を取得する
        /// </summary>
        /// <returns></returns>
        private double Calibration() {
            return ReadData(CALIBRATION_SAMPLE_COUNTS);
        }

        /// <summary>
        /// 呼気中アルコール濃度を取得する
        /// </summary>
        /// <returns></returns>
        private double GetBAC() {
            double read = ReadData(READ_SAMPLE_TIMES);
            return CalculationBAC(read / R0 * R0_CLEAN_AIR_FATOR);
        }

        /// <summary>
        /// 呼気中アルコール濃度を計算する
        /// </summary>
        /// <param name="RsR0Ratio"></param>
        /// <returns></returns>
        private double CalculationBAC(double RsR0Ratio) {
            List<double[]> alcRate = new List<double[]>(11);
            alcRate.Add(new double[] { 1.7, 2.3, -0.2, 0.22 });
            alcRate.Add(new double[] { 1, 1.7, -0.27143, 0.41 });
            alcRate.Add(new double[] { 1, 1.7, -0.27143, 0.41 });
            alcRate.Add(new double[] { 0.55, 1, -1.53333, 1.1 });
            alcRate.Add(new double[] { 0.4, 0.55, -4, 1.7 });
            alcRate.Add(new double[] { 0.29, 0.4, -7.27273, 2.5 });
            alcRate.Add(new double[] { 0.2, 0.29, -16.66667, 4 });
            alcRate.Add(new double[] { 0.17, 0.2, -70, 6.1 });
            alcRate.Add(new double[] { 0.14, 0.17, -63.33333, 8 });
            alcRate.Add(new double[] { 0.12, 0.14, -100, 10 });
            alcRate.Add(new double[] { 0.1, 0.12, -4450, 99 });

            if (RsR0Ratio > alcRate[0][1]) {
                return 0;
            }
            if (RsR0Ratio < alcRate[alcRate.Count - 1][0]) {
                return 999;
            }

            double bsRatio = 0;
            double grad = 0;
            double bacBase = 0;

            foreach (double[] alc in alcRate) {
                if (alc[0] <= RsR0Ratio && RsR0Ratio < alc[1]) {
                    bsRatio = alc[0];
                    grad = alc[2];
                    bacBase = alc[3];
                    break;
                }
            }

            return bacBase + ((RsR0Ratio - bsRatio) * grad);
        }

    }
}