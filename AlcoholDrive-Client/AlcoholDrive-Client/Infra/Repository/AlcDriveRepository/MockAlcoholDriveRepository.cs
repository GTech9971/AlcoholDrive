using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Model.Response;
using AlcoholDrive_Client.Repository;
using AlcoholDrive_Client.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Infra.Repository {

    public class MockAlcoholDriveRepository : AlcoholDriveRepository {

        private bool _isConnect;

        public override bool IsConnect {
            get {
                return _isConnect;
            }
        }

        public MockAlcoholDriveRepository() {
            _isConnect = false;
        }

        public override bool ConnectDrive() {
            _isConnect = true;
            return true;
        }

        public override bool DisconnectDrive() {
            _isConnect = false;
            return true;
        }

        public override void StartScanning() {
        }

        public override void StopScanning() {
        }

        public override List<ushort> ReadingAlcoholValue() {
            return new List<ushort>();
        }
    }
}
