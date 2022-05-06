using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Repository {
    
    public abstract class AlcoholDriveRepository{

        public abstract bool IsConnect { get; }

        /// <summary>
        /// デバイスに接続する
        /// </summary>
        /// <returns></returns>
        public abstract bool ConnectDrive();
        /// <summary>
        /// デバイスとの接続を切断する
        /// </summary>
        /// <returns></returns>
        public abstract bool DisconnectDrive();
        /// <summary>
        /// アルコール検知を開始する
        /// </summary>
        public abstract void StartScanning();
        /// <summary>
        /// アルコール検知を停止する
        /// </summary>
        public abstract void StopScanning();

        /// <summary>
        /// アルコールを検知する
        /// </summary>
        /// <returns>true:アルコール未検知, false:アルコール検知</returns>
        public abstract bool CheckAlcohol();
    }
}
