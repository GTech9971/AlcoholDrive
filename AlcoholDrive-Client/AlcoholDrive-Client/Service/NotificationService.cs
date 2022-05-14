using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Model.Commands;
using AlcoholDrive_Client.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {

    public class NotificationService {

        private readonly NotificationRepository _repository;
        private readonly MessageDeliveryService _deliveryService;

        public NotificationService(NotificationRepository repository,
            MessageDeliveryService deliveryService) {
            this._repository = repository;
            this._deliveryService = deliveryService;

            this._deliveryService.MessageSubject.Subscribe(message => {
                //SlackAPI取得
                if(message.Item1 == NotificationCommands.GET_SLACK_API) {
                    this.GetSlackAPI();
                }

                //SlackAPI登録
                if(message.Item1 == NotificationCommands.REGISTRY_SLACK_API) {
                    this.RegistrySlackAPI(message.Item2);
                }

                //Slack送信
                if(message.Item1 == NotificationCommands.SEND_SLACK) {
                    SendAlcResult sendAlcResult = JsonConvert.DeserializeObject<SendAlcResult>(message.Item2);
                    this.SendSlack(sendAlcResult);
                }
            });
        }


        /// <summary>
        /// SlackAPIを取得する
        /// </summary>
        /// <returns></returns>
        private void GetSlackAPI() {
            string slackAPI = this._repository.GetSlackAPI();
            this._deliveryService.PostCommand(NotificationCommands.GET_SLACK_API_RES, slackAPI);
        }

        /// <summary>
        /// SlackAPIを登録する
        /// </summary>
        /// <param name="slackAPI"></param>
        public void RegistrySlackAPI(string slackAPI) {
            this._repository.RegistrySlackAPI(slackAPI);
        }

        /// <summary>
        /// Slackで通知する
        /// </summary>
        /// <param name="sendAlcResult"></param>
        public void SendSlack(SendAlcResult sendAlcResult) {
            this._repository.SendSlack(sendAlcResult);  
        }

    }
}
