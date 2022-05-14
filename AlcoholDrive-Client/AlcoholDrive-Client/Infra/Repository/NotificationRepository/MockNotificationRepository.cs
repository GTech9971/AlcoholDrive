using AlcoholDrive_Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Infra.Repository.NotificationRepository {
    public class MockNotificationRepository : AlcoholDrive_Client.Repository.NotificationRepository {

        private string _slackAPI;

        public MockNotificationRepository() : base() {
            _slackAPI = "SAMPLE";
        }

        public override string GetSlackAPI() {
            return this._slackAPI;
        }

        public override void RegistrySlackAPI(string slackAPI) {
            this._slackAPI = slackAPI;
        }

        public override void SendSlack(SendAlcResult sendAlcResult) {            
        }
    }
}
