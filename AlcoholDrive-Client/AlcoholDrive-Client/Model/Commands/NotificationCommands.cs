using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model.Commands {
    /// <summary>
    /// クライアントから受信するコマンド
    /// 通知系のコマンド
    /// </summary>
    public class NotificationCommands {
        /// <summary>
        /// SlackのAPIを取得する
        /// </summary>
        public static readonly int GET_SLACK_API = 400;

        /// <summary>
        /// SlackのAPIを取得する(受信)
        /// </summary>
        public static readonly int GET_SLACK_API_RES = 401;

        /// <summary>
        /// SlackAPIを登録する 
        /// </summary>
        public static readonly int REGISTRY_SLACK_API = 410;

        /// <summary>
        /// Slackで検査結果を送信する 
        /// </summary>
        public static readonly int SEND_SLACK = 420;
    }
}
