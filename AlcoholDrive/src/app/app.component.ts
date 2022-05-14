import { Component, OnInit } from '@angular/core';
import { AlertController, ToastController } from '@ionic/angular';
import { Observable } from 'rxjs';
import { NotificationService } from './domain/service/Notification.service';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrls: ['app.component.scss'],
})
export class AppComponent implements OnInit {

  private _slackAPI: string;
  readonly slackAPIObserver: Observable<string>;

  constructor(private notificationService: NotificationService,
    private alertCtrl: AlertController,
    private toastCtrl: ToastController,) {

    this.slackAPIObserver = this.notificationService.SlackAPIObserver;
    this.slackAPIObserver.subscribe(slackAPI => {
      this._slackAPI = slackAPI;
    });

  }

  async ngOnInit() {
    await this.notificationService.fetchSlackAPI();
  }


  async onClickMailSetting() {

  }

  /**
   * SlackAPI設定
   */
  async onClickSlackSetting() {
    await this.notificationService.fetchSlackAPI();

    const alert: HTMLIonAlertElement = await this.alertCtrl.create({
      message: '送信先SlackAPI',
      inputs: [
        {
          name: 'slack',
          type: 'text',
          placeholder: 'SlackAPI',
          value: this._slackAPI
        }
      ],
      buttons: [
        {
          text: 'Cancel',
          role: 'cancel',
          cssClass: 'secondary',
        }, {
          text: 'Ok',
          handler: async (data: { slack: string }) => {
            await this.notificationService.registrySalckAPI(data.slack);
            await this.notificationService.fetchSlackAPI();

            const toast: HTMLIonToastElement = await this.toastCtrl.create({ message: 'SlackAPIを登録しました', duration: 1500 });
            await toast.present();
          }
        }
      ]
    });

    await alert.present();
  }

}
