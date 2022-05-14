using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model {
    public class DeviceCommands {

        /// <summary>
        /// デバイス接続
        /// </summary>
        public static readonly int CONNECT_DEVICE = 200;
        /// <summary>
        /// デバイス切断
        /// </summary>
        public static readonly int DISCONNECT_DEVICE = 210;
        /// <summary>
        /// デバイス接続確認
        /// </summary>
        public static readonly int IS_CONNECT_DEVICE = 220;
        /// <summary>
        /// デバイス接続確認 結果
        /// </summary>
        public static readonly int IS_CONNECT_DEVICE_RES = 221;
    }
}
