using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Model.Response;
using AlcoholDrive_Client.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    public class AlcoholDriveService {

        private AlcDriveState _state;
        private readonly AlcoholDriveRepository repository;
        private readonly MessageDeliveryService deliveryService;

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

            this._state = AlcDriveState.NONE;
            this.repository = repository;
            this.deliveryService = deliveryService;            

            this.deliveryService.MessageSubject.Subscribe(message => {
                int cmd = message.Item1;
                
                //接続
                if(cmd == DeviceCommands.CONNECT_DEVICE) {
                    ConnectDrive();
                }
                //切断
                if(cmd == DeviceCommands.DISCONNECT_DEVICE) {
                    DisconnectDrive();
                }
                //デバイス接続確認
                if(cmd == DeviceCommands.IS_CONNECT_DEVICE) {
                    deliveryService.PostCommand(DeviceCommands.IS_CONNECT_DEVICE_RES, IsConnect);
                }

                //スキャン開始 結果送信
                if(cmd == AlcoholDriveFrontCommands.START_SCANNING) {
                    StartScanning();
                }

                //スキャン停止
                if(cmd == AlcoholDriveFrontCommands.STOP_SCANNING) {
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
                alcDriveResult.DrivableResult = false;
                alcDriveResult.State = AlcDriveState.SCANNING;
                string json = JsonConvert.SerializeObject(alcDriveResult);
                this.deliveryService.PostCommand(AlcoholDriveFrontCommands.SCAN_RESULT, json);


                //結果送信                
                alcDriveResult.DrivableResult = repository.CheckAlcohol();
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
        /// アルコールを検知する
        /// </summary>
        /// <returns>true:アルコール未検知, false:アルコール検知</returns>
        public bool CheckAlcohol() {
            try {
                bool ret = repository.CheckAlcohol();
                return ret;
            } catch (Exception ex) {
                this._state = AlcDriveState.FAIL;
                deliveryService.PostException(ex);
                return false;
            }
        }


    }
}
