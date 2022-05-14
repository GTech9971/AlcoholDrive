using AlcoholDrive_Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Infra.Repository.UserRepository {
    public class UserImplRepository : AlcoholDrive_Client.Repository.UserRepository {

        public UserImplRepository() : base() {

        }

        public override void DeleteUser(int userId) {
            throw new NotImplementedException();
        }

        public override List<UserModel> GetUsers() {
            throw new NotImplementedException();
        }

        public override void RegistryUser(UserModel user) {
            throw new NotImplementedException();
        }

        public override void SetUserBoss(int userId, int bossId) {
            throw new NotImplementedException();
        }
    }
}
