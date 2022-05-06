
using System;

namespace AlcoholDrive_Client.Model {
    /// <summary>
    /// アルコールドライブコマンド
    /// </summary>
    public class AlcoholDriveCommands {
        /// <summary>
        /// アルコール未検知
        /// </summary>
        public static readonly byte ALCOHOL_OK = 0x81;
        /// <summary>
        /// アルコール検知
        /// </summary>
        public static readonly byte ALCOHOL_NG = 0x82;
        /// <summary>
        /// スキャン開始
        /// </summary>
        public static readonly byte START_SCANNING = 0x80;
        /// <summary>
        /// スキャン停止
        /// </summary>
        public static readonly byte STOP_SCANNING = 0x70;
    }
}
