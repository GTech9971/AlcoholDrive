using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model {
    /// <summary>
    /// 設定情報
    /// </summary>
    public class AlcSetting {
        /// <summary>
        /// 送信先SlackAPI
        /// </summary>
        public string SlackAPI { get; set; }
        /// <summary>
        /// 送信先メールアドレス
        /// </summary>
        public string MailAddress { get; set; }

        public AlcSetting() {
            SlackAPI = "";
            MailAddress = "";
        }

    }
}
