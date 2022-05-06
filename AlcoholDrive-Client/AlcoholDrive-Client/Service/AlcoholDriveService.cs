using AlcoholDrive_Client.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    public class AlcoholDriveService {

        private readonly AlcoholDriveRepository repository;

        public AlcoholDriveService(AlcoholDriveRepository repository) {
            this.repository = repository;
        }

    }
}
