using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Model.Exceptions;
using AlcoholDrive_Client.Repository;
using HidApiAdapter;
using System;
using System.Linq;
using System.Threading;

namespace AlcoholDrive_Client.Infra.Repository {

    public class AlcoholDriveImplRepository : AlcoholDriveRepository {
        /// <summary>
        /// デバイスのベンダーID
        /// </summary>
        private const int VID = 0x04D8;
        /// <summary>
        /// デバイスのプロダクトID
        /// </summary>
        private const int PID = 0x003F;
        /// <summary>
        /// データ読み書きのサイズ
        /// </summary>
        private const int DATA_SIZE = 64;

        /// <summary>
        /// HIDデバイスマネージャー
        /// </summary>
        private readonly HidDeviceManager hidDeviceManager;

        /// <summary>
        /// アルコール検知デバイス
        /// </summary>
        private HidDevice alcDevice;


        private bool _isConnect;

        /// <summary>
        /// デバイスとの接続状況
        /// </summary>
        public override bool IsConnect {
            get {
                return _isConnect;
            }
        }

        public AlcoholDriveImplRepository() {
            hidDeviceManager = HidDeviceManager.GetManager();
            _isConnect = false;
        }


        public override bool ConnectDrive() {
            alcDevice = hidDeviceManager.SearchDevices(VID, PID).FirstOrDefault();
            if (alcDevice == null) {
                throw new AlcoholDeviceNotFoundException();
            }

            _isConnect = alcDevice.Connect();

            return _isConnect;
        }

        public override bool DisconnectDrive() {
            if (IsConnect == false || alcDevice == null) {
                _isConnect = false;
                return false;
            }

            if (alcDevice.Disconnect() == false) {
                return false;
            } else {
                _isConnect = false;
                return true;
            }
        }

        /// <summary>
        /// デバイスに書き込む
        /// </summary>
        /// <param name="cmds"></param>
        /// <returns></returns>
        /// <exception cref="AlcoholDeviceNotFoundException"></exception>
        /// <exception cref="AlcoholDeviceIOException"></exception>
        private bool Write2Device(byte[] cmds) {
            if (_isConnect == false) {
                throw new AlcoholDeviceNotFoundException();
            }

            if (alcDevice == null) {
                this.ConnectDrive();
            }

            if (alcDevice.Write(cmds) == -1) {
                throw new AlcoholDeviceIOException(alcDevice?.Manufacturer(), alcDevice?.Product());
            }

            return true;
        }

        /// <summary>
        /// デバイスからデータを読み取る
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="AlcoholDeviceNotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="AlcoholDeviceIOException"></exception>
        private bool Read2Device(ref byte[] data) {
            if (_isConnect == false) {
                throw new AlcoholDeviceNotFoundException();
            }

            if (this.alcDevice == null) {
                this.ConnectDrive();
            }

            if (data.Length != DATA_SIZE) {
                throw new ArgumentException("データ格納サイズが64以外");
            }

            if (alcDevice.Read(data, data.Length) == -1) {
                throw new AlcoholDeviceIOException(alcDevice?.Manufacturer(), alcDevice?.Product());
            }

            return true;
        }

        public override void StartScanning() {
            byte[] cmds = new byte[DATA_SIZE];
            cmds[0] = AlcoholDriveCommands.START_SCANNING;
            Write2Device(cmds);

            //TODO なくてもいい 雰囲気のため
            Thread.Sleep(1000);
        }

        public override void StopScanning() {
            byte[] cmds = new byte[DATA_SIZE];
            cmds[0] = AlcoholDriveCommands.STOP_SCANNING;
            Write2Device(cmds);

            //TODO なくてもいい 雰囲気のため
            Thread.Sleep(1000);
        }

        public override bool CheckAlcohol() {
            byte[] data = new byte[DATA_SIZE];
            Read2Device(ref data);

            if (data[0] == AlcoholDriveCommands.ALCOHOL_OK) {
                return true;
            }

            if (data[0] == AlcoholDriveCommands.ALCOHOL_NG) {
                return false;
            }

            throw new AlcoholDeviceIOException("データの読み取りに失敗");
        }
    }
}
