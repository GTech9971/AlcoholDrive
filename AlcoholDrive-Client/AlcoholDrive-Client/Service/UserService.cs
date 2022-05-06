using AlcoholDrive_Client.Model;
using AlcoholDrive_Client.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    /// <summary>
    /// ユーザサービス
    /// </summary>
    public class UserService : BaseService {

        private readonly UserRepository repository;
        private readonly MessageDeliveryService deliveryService;

        public UserService(UserRepository repository,
            MessageDeliveryService deliveryService) : base() {
            this.deliveryService = deliveryService;
            this.repository = repository;

            //購読
            this.deliveryService.MessageSubject.Subscribe(message => {
                if (ContainsCommand(message.Item1) == false) { return; }
                int cmd = message.Item1;
                //ユーザ取得
                if (cmd == UserCommands.GET_USER) {
                    var users = GetUsers();
                    deliveryService.PostCommand(UserCommands.GET_USER_RES, users);
                }

                //ユーザ登録
                if (cmd == UserCommands.REGISTRY_USER) {
                    var user = JsonConvert.DeserializeObject<UserModel>(message.Item2);
                    RegistryUser(user);
                }

                //ユーザ削除
                if (cmd == UserCommands.DEL_USER) {
                    var user = JsonConvert.DeserializeObject<UserModel>(message.Item2);
                    DeleteUser(user.UserId);
                    var users = GetUsers();
                }

                //上長設定
                if (cmd == UserCommands.SET_USER_BOSS) {
                    var contexts = message.Item2.Split(',');
                    int userId = int.Parse(contexts[0]);
                    int bossId = int.Parse(contexts[1]);
                    SetUserBoss(userId, bossId);
                }
            });
        }

        protected override void InitCommandList() {
            CommandList.AddRange(new int[] { UserCommands.GET_USER, UserCommands.GET_USER_RES, UserCommands.REGISTRY_USER, UserCommands.DEL_USER, UserCommands.SET_USER_BOSS });
        }


        /// <summary>
        /// ユーザ一覧を返す
        /// </summary>
        /// <returns></returns>
        public List<UserModel> GetUsers() {
            return repository.GetUsers();
        }

        /// <summary>
        /// ユーザを登録する
        /// </summary>
        /// <param name="user"></param>
        public void RegistryUser(UserModel user) {
            repository.RegistryUser(user);
        }

        /// <summary>
        /// ユーザを削除する
        /// </summary>
        /// <param name="userId"></param>
        public void DeleteUser(int userId) {
            repository.DeleteUser(userId);
        }

        /// <summary>
        /// 上長を設定する
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bossId"></param>
        public void SetUserBoss(int userId, int bossId) {
            repository.SetUserBoss(userId, bossId);
        }


    }
}