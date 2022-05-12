import { Component, Input, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastController } from "@ionic/angular";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { UserModel } from "src/app/domain/model/User.model";
import { AlcDriveService } from "src/app/domain/service/AlcDrive.service";

@Component({
    selector: 'alc-check-result',
    templateUrl: './alc-check-result.component.html',
    styleUrls: ['./alc-check-result.component.scss']
})
export class AlcCheckResultComponent implements OnInit {

    @Input() user: UserModel;

    @Input() alcDriveResult: AlcDriveResultodel;

    constructor(private alcDriveService: AlcDriveService,
        private toastCtrl: ToastController,
        private router: Router) { }

    ngOnInit(): void {
    }

    private async showToast() {
        const toast: HTMLIonToastElement = await this.toastCtrl.create({ message: '検査結果を送信しました', duration: 1500 });
        await toast.present();
    }

    /**
     * 検査結果をメールで送る
     */
    async onClickSendResultMail() {
        // TODO
        await this.showToast();
        await this.router.navigate(['home']);
    }

    /**
     * 検査結果をSlackで送る
     */
    async onClickSendResultSlack() {
        // TODO
        await this.showToast();
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