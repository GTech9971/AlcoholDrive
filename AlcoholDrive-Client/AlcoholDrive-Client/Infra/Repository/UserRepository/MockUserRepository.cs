using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Infra.Repository {
    public class MockUserRepository : AlcoholDrive_Client.Repository.UserRepository {

        private static readonly List<UserModel> _memory = new List<UserModel>();

        public MockUserRepository() : base() {
            _memory.Add(new UserModel() {
                UserId = 0,
                UserName = "SAMPLE",
            });
        }

        public override void DeleteUser(int userId) {
            if (_memory.Exists(u => u.UserId == userId)) {
                var del = _memory.Find(u => u.UserId == userId);
                _memory.Remove(del);
            }
        }

        public override List<UserModel> GetUsers() {
            return _memory.ToList();
        }

        public override void RegistryUser(UserModel user) {
            int maxId = 0;
            if (_memory.Any()) {
                maxId = _memory.Max(u => u.UserId) + 1;
            }
            user.UserId = maxId;
            _memory.Add(user);
        }

        public override void SetUserBoss(int userId, int bossId) {
            var user = _memory.FirstOrDefault(u => u.UserId == userId);
            var boss = _memory.FirstOrDefault(u => u.UserId == bossId);

            if (user == null || boss == null) {
                throw new Exception("ユーザが見つからない");
            }

            user.BossId = bossId;
            int userIndex = _memory.IndexOf(user);
            _memory[userIndex] = user;
        }
    }
}
