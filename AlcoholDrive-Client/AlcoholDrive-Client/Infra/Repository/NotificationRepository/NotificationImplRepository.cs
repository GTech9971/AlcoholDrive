using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Service;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace AlcoholDrive_Client.Infra.Repository.NotificationRepository {
    public class NotificationImplRepository : AlcoholDrive_Client.Repository.NotificationRepository {

        private static readonly string SETTING_PATH = "setting.json";

        public NotificationImplRepository() : base() {
        }

        private void Save(AlcSetting setting) {
            string json = JsonConvert.SerializeObject(setting);
            using (var writer = new StreamWriter(SETTING_PATH, false, Encoding.UTF8)) {
                writer.WriteLine(json);
                writer.Flush();
            }
        }

        private AlcSetting Load() {
            if (File.Exists(SETTING_PATH) == false) {
                return new AlcSetting();
            }

            using (var reader = new StreamReader(SETTING_PATH, Encoding.UTF8)) {
                string json = reader.ReadToEnd();
                AlcSetting setting = JsonConvert.DeserializeObject<AlcSetting>(json);
                return setting;
            }
        }

        public override string GetSlackAPI() {
            if (File.Exists(SETTING_PATH) == false) {
                return "";
            }

            AlcSetting setting = this.Load();
            return setting.SlackAPI;
        }

        public override void RegistrySlackAPI(string slackAPI) {
            AlcSetting setting = this.Load();
            setting.SlackAPI = slackAPI;
            Save(setting);
        }

        public override void SendSlack(SendAlcResult sendAlcResult) {
            PostMessage postMessage = new PostMessage();
            string resultText = sendAlcResult.AlcCheckResult ? "合格" : "不合格";
            postMessage.text = $"検査者:{sendAlcResult.User.UserName} 検査結果:{resultText} 呼気中アルコール濃度:{sendAlcResult.BAC}[mg/L]";

            AlcLogService.Write(postMessage.text);

            string slackAPI = this.GetSlackAPI();

            string json = JsonConvert.SerializeObject(postMessage);
            //以下がなんかいるらしい
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            using (HttpClient client = new HttpClient()) {
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var res = client.PostAsync(slackAPI, content).Result;
            }
        }
    }


    class PostMessage {
        public string text { get; set; }

        public PostMessage() {
            text = "";
        }
    }

}

