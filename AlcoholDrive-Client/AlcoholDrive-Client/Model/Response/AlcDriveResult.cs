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
        /// <summary>
        /// 呼気中アルコール濃度を返す mg\L
        /// </summary>
        public double BAC { get; set; }

        public AlcDriveResult() {
            State = AlcDriveState.NONE;
            BAC = 0.0;
            DrivableResult = false;
        }
    }
}
