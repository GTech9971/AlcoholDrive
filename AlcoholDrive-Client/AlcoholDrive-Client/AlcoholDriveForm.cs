﻿using AlcoholDrive_Client.Repository;
using AlcoholDrive_Client.Service;
using Microsoft.Web.WebView2.Core;
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

        private const string FOLDER_PATH = @"C:\Users\georg\Documents\GitHub\AlcoholDrive\AlcoholDrive\www";
        private const string URL_PATH = @"https://alcdrive.com/index.html";

        private readonly AlcoholDriveService alcService;
        private readonly UserService userService;
        private readonly MessageDeliveryService deliveryService;

        public AlcoholDriveForm(AlcoholDriveRepository alcoholDriveRepository,
            UserRepository userRepository) {

            InitializeComponent();

            deliveryService = new MessageDeliveryService(webView21);
            alcService = new AlcoholDriveService(alcoholDriveRepository, deliveryService);
            userService = new UserService(userRepository, deliveryService);


            webView21.WebMessageReceived += WebView21_WebMessageReceived;
        }

        private async void AlcoholDriveForm_Load(object sender, EventArgs e) {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.SetVirtualHostNameToFolderMapping("alcdrive.com", FOLDER_PATH, CoreWebView2HostResourceAccessKind.Allow);
            webView21.CoreWebView2.Navigate(URL_PATH);            
            alcService.ConnectDrive();

            await Task.Delay(1000);

            deliveryService.PostCommand(20, "sample");
        }

        /// <summary>
        /// フロントからメッセージを受信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebView21_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e) {
            deliveryService.RecievedMessage(e.TryGetWebMessageAsString());
        }


    }
}
