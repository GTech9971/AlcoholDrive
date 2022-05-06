using Microsoft.Web.WebView2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    public class MessageDeliveryService {

        private readonly WebView2 webView2;



        public MessageDeliveryService(WebView2 webView2) {
            this.webView2 = webView2;
        }

        private void PostMessage(string message) {
            webView2.CoreWebView2.PostWebMessageAsString(message);
        }

        public void PostException(Exception ex) {

        }

        public void PostCommand(int cmd) {

        }

    }
}
