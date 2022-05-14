using AlcoholDrive_Client.Infra.Repository;
using AlcoholDrive_Client.Infra.Repository.NotificationRepository;
using AlcoholDrive_Client.Infra.Repository.UserRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlcoholDrive_Client {
    internal static class Program {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new AlcoholDriveForm(
                new AlcoholDriveImplRepository(),
                new UserImplRepository(),
                new NotificationImplRepository()
                ));
        }
    }
}
