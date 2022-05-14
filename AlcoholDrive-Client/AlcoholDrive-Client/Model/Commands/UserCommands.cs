using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model {
    /// <summary>
    /// ユーザコマンド
    /// </summary>
    public class UserCommands {
        /// <summary>
        /// ユーザ取得
        /// </summary>
        public static readonly int GET_USER = 100;
        /// <summary>
        /// ユーザ取得結果
        /// </summary>
        public static readonly int GET_USER_RES = 101;
        /// <summary>
        /// ユーザ登録
        /// </summary>
        public static readonly int REGISTRY_USER = 110;
        /// <summary>
        /// ユーザ削除
        /// </summary>
        public static readonly int DEL_USER = 120;
        /// <summary>
        /// 上長設定
        /// </summary>
        public static readonly int SET_USER_BOSS = 130;
    }
}
