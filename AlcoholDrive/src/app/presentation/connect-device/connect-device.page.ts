import { Component, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import { ToastController } from "@ionic/angular";
import { Observable } from "rxjs";
import { AlcDriveState } from "src/app/domain/model/AlcDriveState.model";
import { AlcDriveService } from "src/app/domain/service/AlcDrive.service";

@Component({
    selector: 'alc-connect-device',
    templateUrl: './connect-device.page.html',
    styleUrls: ['./connect-device.page.scss']
})
export class ConnectDevicePage implements OnDestroy {

    private readonly alcDriveStateObserver: Observable<AlcDriveState>;

    clickFlg: boolean = false;

    constructor(private alcService: AlcDriveService,
        private toastCtrl:ToastController,
        private router: Router) {

        this.alcDriveStateObserver = this.alcService.AlcDriveStateObserver;
        this.alcDriveStateObserver.subscribe(async state => {
            if (state === AlcDriveState.CONNECTED) {
                const toast:HTMLIonToastElement = await this.toastCtrl.create({message:'デバイスに接続しました。', duration:1500});
                await toast.present();
                await this.router.navigate(['home']);
            }
        });
    }

    ngOnDestroy(): void {
        this.clickFlg = false;
    }


    async onClickConnectDevice() {
        this.clickFlg = !this.clickFlg;
        if (this.clickFlg) {
            this.alcService.ConnectDevice();
        }
    }
}