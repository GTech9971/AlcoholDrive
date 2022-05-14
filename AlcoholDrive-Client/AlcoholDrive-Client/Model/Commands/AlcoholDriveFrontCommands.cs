using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model {
    /// <summary>
    /// フロントから受信するアルコール検知コマンド
    /// </summary>
    public class AlcoholDriveFrontCommands {
        /// <summary>
        /// スキャン開始
        /// </summary>
        public static readonly int START_SCANNING = 300;
        /// <summary>
        /// スキャン停止
        /// </summary>
        public static readonly int STOP_SCANNING = 310;
        /// <summary>
        /// 検査結果
        /// </summary>
        public static readonly int SCAN_RESULT = 320;

    }
}
