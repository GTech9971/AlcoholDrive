using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    public class AlcoholDriveService {

        private readonly AlcoholDriveRepository repository;

        public readonly Subject<int> CommandsSubject;

        public readonly Subject<Exception> ErrorSubject;

        public AlcoholDriveService(AlcoholDriveRepository repository) {
            this.repository = repository;
            CommandsSubject = new Subject<int>();
            ErrorSubject = new Subject<Exception>();
        }


        public bool IsConnect {
            get {
                return repository.IsConnect;
            }
        }

        /// <summary>
        /// デバイスに接続する
        /// </summary>
        /// <returns></returns>
        public bool ConnectDrive() {
            try {
                return repository.ConnectDrive();
            } catch (Exception ex) {
                ErrorSubject.OnNext(ex);
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
                ErrorSubject.OnNext(ex);
                return false;
            }
        }

        /// <summary>
        /// アルコール検知を開始する
        /// </summary>
        public void StartScanning() {
            try {
                repository.StartScanning();
                CommandsSubject.OnNext(AlcoholDriveCommands.START_SCANNING);
            } catch (Exception ex) {
                ErrorSubject.OnNext(ex);
            }
        }

        /// <summary>
        /// アルコール検知を停止する
        /// </summary>
        public void StopScanning() {
            try {
                repository.StopScanning();
                CommandsSubject.OnNext(AlcoholDriveCommands.STOP_SCANNING);
            } catch (Exception ex) {
                ErrorSubject.OnNext(ex);
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
                ErrorSubject.OnNext(ex);
                return false;
            }
        }

    }
}
