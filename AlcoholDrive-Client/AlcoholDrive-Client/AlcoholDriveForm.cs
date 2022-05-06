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

        public AlcoholDriveForm() {
            InitializeComponent();            
        }

        private async void AlcoholDriveForm_Load(object sender, EventArgs e) {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.Navigate("https://www.google.com/webhp?hl=ja&sa=X&ved=0ahUKEwjqtNXD_Mj3AhXvxIsBHQMkD8cQPAgI");
        }
    }
}
