using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model.Response {
    /// <summary>
    /// 検知結果を格納する
    /// </summary>
    public class AlcDriveResult {
        /// <summary>
        /// センサーの状態
        /// </summary>
        public AlcDriveState State { get; set; }
        /// <summary>
        /// true:運転可能, false:運転不可能
        /// </summary>
        public bool DrivableResult { get; set; }

        public AlcDriveResult() {
            State = AlcDriveState.NONE;
            DrivableResult = false;
        }
    }
}
