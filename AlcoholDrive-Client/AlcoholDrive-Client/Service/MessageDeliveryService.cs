using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace AlcoholDrive_Client.Service {
    /// <summary>
    /// フロントとのデータ連携を行うサービス
    /// </summary>
    public class MessageDeliveryService {

        private static readonly int EXCEPTION_CMD = 999;

        private readonly WebView2 webView2;

        /// <summary>
        /// フロントからのメッセージのサブジェクト
        /// </summary>
        public readonly Subject<Tuple<int, string>> MessageSubject;

        public MessageDeliveryService(WebView2 webView2) {
            this.webView2 = webView2;
            MessageSubject = new Subject<Tuple<int, string>>();
        }

        private void PostMessage(string message) {
            webView2.CoreWebView2.PostWebMessageAsString(message);
        }

        /// <summary>
        /// フロントにエラーメッセージを送信する
        /// </summary>
        /// <param name="ex"></param>
        public void PostException(Exception ex) {
            PostMessage($"{EXCEPTION_CMD}={ex.Message}");
        }

        /// <summary>
        /// フロントにコマンドとjsonオブジェクトを送信する
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="jsonStr"></param>
        public void PostCommand(int cmd, string jsonStr) {
            PostMessage($"{cmd}={jsonStr}");
        }

        /// <summary>
        /// フロントにコマンドとjsonオブジェクトを送信する
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="jsonObject"></param>
        public void PostCommand(int cmd, object jsonObject) {
            var json = JsonConvert.SerializeObject(jsonObject);
            PostMessage($"{cmd}={json}");
        }


        /// <summary>
        /// フロントからコマンドと、jsonオブジェクトを取得する
        /// </summary>
        /// <param name="message"></param>
        public void RecievedMessage(string message) {
            string[] context = message.Split('=');
            string cmdStr = context[0];
            int cmd = int.Parse(cmdStr);

            MessageSubject.OnNext(new Tuple<int, string>(cmd, context[1]));
        }

    }
}
