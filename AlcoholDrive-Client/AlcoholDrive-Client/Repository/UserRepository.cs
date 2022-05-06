using AlcoholDrive_Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Repository {

    public abstract class UserRepository {

        /// <summary>
        /// ユーザ一覧を返す
        /// </summary>
        /// <returns></returns>
        public abstract List<UserModel> GetUsers();
        /// <summary>
        /// ユーザを登録する
        /// </summary>
        /// <param name="user"></param>
        public abstract void RegistryUser(UserModel user);
        /// <summary>
        /// ユーザを削除する
        /// </summary>
        /// <param name="userId"></param>
        public abstract void DeleteUser(int userId);
        /// <summary>
        /// 上長を設定する
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="bossId"></param>
        public abstract void SetUserBoss(int userId, int bossId);
    }
}
