using AlcoholDrive_Client.Repository;
using AlcoholDrive_Client.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlcoholDrive_Client {
    public partial class AlcoholDriveForm : Form {

        private readonly AlcoholDriveService alcService;

        public AlcoholDriveForm(AlcoholDriveRepository alcoholDriveRepository) {
            InitializeComponent();
            alcService = new AlcoholDriveService(alcoholDriveRepository);

            //エラー購読
            alcService.ErrorSubject.Subscribe(ex => {
                webView21.CoreWebView2.PostWebMessageAsString()
            });
            //コマンド購読
            alcService.CommandsSubject.Subscribe(cmd => {

            });
        }


        private async void AlcoholDriveForm_Load(object sender, EventArgs e) {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.Navigate("https://www.google.com/webhp?hl=ja&sa=X&ved=0ahUKEwjqtNXD_Mj3AhXvxIsBHQMkD8cQPAgI");

            alcService.ConnectDrive();
        }
    }
}
