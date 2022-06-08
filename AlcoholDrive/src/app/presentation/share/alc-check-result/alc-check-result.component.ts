import { Component, Input, OnDestroy, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastController } from "@ionic/angular";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { SendAlcResultModel } from "src/app/domain/model/SendAlcResult.model";
import { UserModel } from "src/app/domain/model/User.model";
import { AlcDriveService } from "src/app/domain/service/AlcDrive.service";
import { NotificationService } from "src/app/domain/service/Notification.service";

@Component({
    selector: 'alc-check-result',
    templateUrl: './alc-check-result.component.html',
    styleUrls: ['./alc-check-result.component.scss']
})
export class AlcCheckResultComponent implements OnInit, OnDestroy {

    @Input() user: UserModel;

    @Input() alcDriveResult: AlcDriveResultodel;

    /** Slackが有効かどうか */
    get IsEnableSlack(): boolean {
        return this.notificationService.IsEnableSlack;
    }

    /** メールが有効かどうか */
    get IsEnableMail(): boolean {
        //TODO 
        return false;
    }

    constructor(private alcDriveService: AlcDriveService,
        private notificationService: NotificationService,
        private toastCtrl: ToastController,
        private router: Router) { }



    ngOnInit(): void {
    }

    async ngOnDestroy() {
        await this.alcDriveService.stopScanning();
    }

    private async showToast() {
        const toast: HTMLIonToastElement = await this.toastCtrl.create({ message: '検査結果を送信しました', duration: 1500 });
        await toast.present();
    }

    /**
     * 検査結果をメールで送る
     */
    async onClickSendResultMail() {
        if (this.IsEnableMail === false) { return; }

        await this.alcDriveService.stopScanning();

        await this.showToast();
        await this.router.navigate(['home']);
    }

    /**
     * 検査結果をSlackで送る
     */
    async onClickSendResultSlack() {
        if (this.IsEnableSlack === false) { return; }

        await this.alcDriveService.stopScanning();

        const result: SendAlcResultModel = {
            AlcCheckResult: this.alcDriveResult.DrivableResult,
            BAC: this.alcDriveResult.BAC,
            User: this.user,
            Message: ''
        };

        await this.notificationService.sendSlack(result);
        await this.showToast();
        await this.router.navigate(['home']);
    }

    /** ホームに戻る */
    async onClickBackHome() {
        await this.alcDriveService.stopScanning();
        await this.router.navigate(['home']);
    }


    /**
     * 再検査ボタン
     */
    async onClickReCheckButton() {
        await this.alcDriveService.stopScanning();
        await this.alcDriveService.startScanning();
    }

}