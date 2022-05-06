using AlcoholDrive_Client.Infra.Repository;
using System;
using System.Collections.Generic;
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
                new MockAlcoholDriveRepository(),
                new MockUserRepository()));
        }
    }
}
