using AlcoholDrive_Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Repository {
    public abstract class NotificationRepository {
        /// <summary>
        /// SlackAPIを取得する
        /// </summary>
        /// <returns></returns>
        public abstract string GetSlackAPI();

        /// <summary>
        /// SlackAPIを登録する
        /// </summary>
        /// <param name="slackAPI"></param>
        public abstract void RegistrySlackAPI(string slackAPI);

        /// <summary>
        /// Slackで通知する
        /// </summary>
        /// <param name="sendAlcResult"></param>
        public abstract void SendSlack(SendAlcResult sendAlcResult);
    }
}
