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

        private const int CALIBRATION_SAMPLE_COUNTS = 50;

        private const int READ_SAMPLE_TIMES = 2500;

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

        /// <summary>
        /// デバイスからアナログ値を取得する
        /// </summary>
        /// <param name="sampleCounts">取得回数 * 32</param>
        /// <returns></returns>
        private double ReadData(int sampleCounts) {
            const int DATA_SIZE = 64;
            List<ushort> values = new List<ushort>(DATA_SIZE / 2 * sampleCounts);
            //データ受信を行う
            for (int i = 0; i < sampleCounts; i++) {
                values.AddRange(this.repository.ReadingAlcoholValue());
            }


            AlcLogService.Write("生データ");
            values.ForEach(v => {
                AlcLogService.Write(message: v.ToString(), headerLess: true);
            });


            AlcLogService.Write("加工データ");
            //四分位範囲を行う
            values = InterquartileRange(values);
            values.ForEach(v => {
                AlcLogService.Write(message: v.ToString(), headerLess: true);
            });

            double avgValue = 0;
            //平均値の算出
            values.ToList().ForEach(v => {
                //avgValue += (RL_VALUE * (1024 - v) / (double)(v));
                avgValue += v;
            });
            avgValue = avgValue / values.Count;

            return avgValue;
        }

        /// <summary>
        /// 四分位範囲を行う
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<ushort> InterquartileRange(List<ushort> list) {
            //昇順にソート
            list.Sort((a, b) => { return a - b; });

            //Q2
            ushort q2 = 0;
            if (list.Count % 2 == 0) {
                q2 = (ushort)((list[list.Count - 1 / 2] + list[list.Count / 2]) / 2.0);
            } else {
                q2 = list[list.Count / 2];
            }

            //Q1
            int q1Index = list.Count / 4;
            ushort q1 = list[q1Index];
            //Q3
            int q3Index = list.Count - q1Index;
            ushort q3 = list[q3Index];
            //IQR
            ushort iqr = (ushort)(q3 - q1);

            List<ushort> result = new List<ushort>();
            foreach (ushort a in list) {
                if (q1 - (iqr * 1.5) >= a) { continue; }
                if (q3 + (iqr * 1.5) <= a) { continue; }
                result.Add(a);
            }

            return result;
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
            double volt = ReadData(CALIBRATION_SAMPLE_COUNTS);
            volt = (volt / 1024.0) * 5.0;

            AlcLogService.Write($"Volt = {volt}");

            double RS = ((5.0 * RL_VALUE) / volt) - RL_VALUE;
            return RS / 60.0;
        }

        /// <summary>
        /// 呼気中アルコール濃度を取得する
        /// </summary>
        /// <returns></returns>
        private double GetBAC() {
            double volt = ReadData(READ_SAMPLE_TIMES);
            volt = (volt / 1024.0) * 5.0;

            double RS_gas = ((5.0 * RL_VALUE) / volt) - RL_VALUE;
            /*-Replace the value of R0 with the value of R0 in your test -*/
            R0 = 143.0;
            double ratio = RS_gas / R0;// ratio = RS/R0
            double x = 0.4 * ratio;
            return Math.Pow(x, -1.431);  //BAC in mg/L
        }



        /// <summary>
        /// 呼気中アルコール濃度を計算する
        /// </summary>
        /// <param name="RsR0Ratio"></param>
        /// <returns></returns>
        private double CalculationBAC(double RsR0Ratio) {

            AlcLogService.Write($"Ration = {RsR0Ratio}");

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