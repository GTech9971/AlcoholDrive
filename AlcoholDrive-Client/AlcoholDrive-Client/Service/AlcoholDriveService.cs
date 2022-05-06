using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    public class AlcoholDriveService :BaseService{

        private readonly AlcoholDriveRepository repository;
        private readonly MessageDeliveryService deliveryService;

        public bool IsConnect {
            get {
                return repository.IsConnect;
            }
        }

        public AlcoholDriveService(AlcoholDriveRepository repository,
            MessageDeliveryService deliveryService) : base() {
            
            this.repository = repository;
            this.deliveryService = deliveryService;            

            this.deliveryService.MessageSubject.Subscribe(message => {
                int cmd = message.Item1;
                if(ContainsCommand(cmd) == false) { return; }

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
            });
        }

        protected override void InitCommandList() {
            CommandList.AddRange(new int[] { DeviceCommands.CONNECT_DEVICE, DeviceCommands.DISCONNECT_DEVICE, DeviceCommands.IS_CONNECT_DEVICE, DeviceCommands.IS_CONNECT_DEVICE_RES });
        }

        /// <summary>
        /// デバイスに接続する
        /// </summary>
        /// <returns></returns>
        public bool ConnectDrive() {
            try {
                return repository.ConnectDrive();
            } catch (Exception ex) {
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
                return repository.DisconnectDrive();
            } catch (Exception ex) {
                deliveryService.PostException(ex);
                return false;
            }
        }

        /// <summary>
        /// アルコール検知を開始する
        /// </summary>
        public void StartScanning() {
            try {
                repository.StartScanning();
            } catch (Exception ex) {
                deliveryService.PostException(ex);
            }
        }

        /// <summary>
        /// アルコール検知を停止する
        /// </summary>
        public void StopScanning() {
            try {
                repository.StopScanning();
            } catch (Exception ex) {
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
                deliveryService.PostException(ex);
                return false;
            }
        }


    }
}
