using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {

    public class AlcLogService {

        private static readonly string LOG_PATH = "./log";

        /// <summary>
        /// ログを書き込む
        /// </summary>
        /// <param name="message"></param>
        /// <param name="headerLess">true:ヘッダーを書き込まない, false:ヘッダー込みで書き込む(デフォルト)</param>
        public static void Write(string message, bool headerLess = false) {
            DateTime now = DateTime.Now;
            string fileName = $"{now.ToString("yyyyMMdd")}.log";
            string path = Path.Combine(LOG_PATH, fileName);

            if(Directory.Exists(LOG_PATH) == false) {
                Directory.CreateDirectory(LOG_PATH);
            }

            using(var writer = new StreamWriter(path, true, Encoding.UTF8)) {
                string header = $"[{now.ToString("yyyy-MM-dd")} {now.ToString("HH:mm:ss")}]";
                //ヘッダーを空にする
                if (headerLess) {
                    header = "";
                }
                writer.WriteLine($"{header} - {message}");
                writer.Flush();
            }
        }
    }
}
