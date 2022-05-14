using AlcoholDrive_Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Infra.Repository.NotificationRepository {
    public class NotificationImplRepository : AlcoholDrive_Client.Repository.NotificationRepository {

        public NotificationImplRepository() : base() {

        }

        public override string GetSlackAPI() {
            throw new NotImplementedException();
        }

        public override void RegistrySlackAPI(string slackAPI) {
            throw new NotImplementedException();
        }

        public override void SendSlack(SendAlcResult sendAlcResult) {
            string slackAPI = this.GetSlackAPI();
            //TODO 通知処理

            string json = "";
            using (HttpClient client = new HttpClient()) {
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var res = client.PostAsync(slackAPI, content).Result;
            }
        }
    }
}
