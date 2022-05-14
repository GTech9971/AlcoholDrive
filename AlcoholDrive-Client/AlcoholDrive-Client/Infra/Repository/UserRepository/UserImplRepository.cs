using AlcoholDrive_Client.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Infra.Repository.UserRepository {
    public class UserImplRepository : AlcoholDrive_Client.Repository.UserRepository {

        public static readonly string USER_DATA_PATH = "./users.json";

        public UserImplRepository() : base() {
        }

        private int GetMaxUserId() {
            List<UserModel> users = GetUsers();
            if(users.Any() == false) { return 1; }
            int maxId = 1;
            users.ForEach(u => {
                if(maxId < u.UserId) {
                    maxId = u.UserId;
                }
            });
            maxId++;
            return maxId;
        }

        private void Save(List<UserModel> users) {
            string json = JsonConvert.SerializeObject(users);
            using(var writer = new StreamWriter(USER_DATA_PATH, false, Encoding.UTF8)) {
                writer.WriteLine(json);
                writer.Flush();
            }
        }

        public override void DeleteUser(int userId) {
            List<UserModel> users = this.GetUsers();
            users = users.Where(u => u.UserId != userId).ToList();
            Save(users);
        }

        public override List<UserModel> GetUsers() {
            if(File.Exists(USER_DATA_PATH) == false) {
                return new List<UserModel>();
            }

            using(var reader = new StreamReader(USER_DATA_PATH, Encoding.UTF8)) {
                string json = reader.ReadToEnd();
                List<UserModel> list = JsonConvert.DeserializeObject<List<UserModel>>(json);
                return list;
            }
        }

        public override void RegistryUser(UserModel user) {
            List<UserModel> users = this.GetUsers();
            user.UserId = GetMaxUserId();
            users.Add(user);            
            Save(users);
        }

        public override void SetUserBoss(int userId, int bossId) {
            throw new NotImplementedException();
        }
    }
}
