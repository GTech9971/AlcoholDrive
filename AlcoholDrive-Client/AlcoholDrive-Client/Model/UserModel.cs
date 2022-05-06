using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Model {
    /// <summary>
    /// ユーザモデル
    /// </summary>
    public class UserModel {

        /// <summary>
        /// ユーザID
        /// </summary>
        [Key]
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// ユーザ名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// ユーザ画像パス
        /// </summary>
        public string UserImagePath { get; set; }

        /// <summary>
        /// 上長ID
        /// </summary>
        [ForeignKey(nameof(UserBoss))]
        public int BossId { get; set; }

        /// <summary>
        /// 上長モデル
        /// </summary>
        public UserModel UserBoss { get; set; }
    }
}
