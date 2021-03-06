using AlcoholDrive_Client.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Repository {

    public abstract class AlcoholDriveRepository {

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
        /// アルコール値を取得する
        /// </summary>
        /// <returns></returns>
        public abstract List<ushort> ReadingAlcoholValue();
    }
}
