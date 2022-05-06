using AlcoholDrive_Client.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override bool CheckAlcohol() {
            return true;
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
    }
}
