using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model {
    /// <summary>
    /// 検査結果の送信に使用するモデル
    /// </summary>
    public class SendAlcResult {
        /// <summary>
        /// 検査結果
        /// </summary>
        public bool AlcCheckResult { get; set; }
        /// <summary>
        /// 検査をしたユーザ
        /// </summary>
        public UserModel User { get; set; }
        /// <summary>
        /// メッセージ
        /// </summary>
        public string Message { get; set; }
    }
}
