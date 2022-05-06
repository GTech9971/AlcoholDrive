import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { AlcDriveState } from "src/app/domain/model/AlcDriveState.model";
import { UserModel } from "src/app/domain/model/User.model";
import { AlcDriveService } from "src/app/domain/service/AlcDrive.service";
import { UserService } from "src/app/domain/service/User.service";

@Component({
    selector: 'alc-alcohol-check-page',
    templateUrl: './alcohol-check.page.html',
    styleUrls: ['./alcohol-check.page.scss']
})
export class AlcoholCheckPage implements OnInit, OnDestroy {

    user: UserModel;

    alcDriveResult: AlcDriveResultodel;

    get STATE_SCANNING(): AlcDriveState { return AlcDriveState.SCANNING; }
    get STATE_OK(): AlcDriveState { return AlcDriveState.OK; }

    constructor(private userService: UserService,
        private alcDriveService: AlcDriveService,
        private route: ActivatedRoute) {
        // TODO サービスから取得する
        this.alcDriveResult = {
            State: AlcDriveState.CONNECTED,
            DrivableResult: false,
        }
    }


    async ngOnInit() {
        this.route.queryParamMap.subscribe(async params => {
            let id: number = Number.parseInt(params.get('userid'));
            this.user = await this.userService.getUser(id);
        });

        // this.alcDriveResult.State = await this.alcDriveService.fetchSensorState();
        // // TODO:Backgroundでチェックする
        // if (this.alcDriveResult.State === AlcDriveState.CONNECTED) {
        //     this.alcDriveResult.State = await this.alcDriveService.startScanning();
        //     if (this.alcDriveResult.State === AlcDriveState.SCANNING) {
        //         //this.alcDriveResult = await this.alcDriveService.fetchAlcDriveResult();
        //     }
        // }

    }

    ngOnDestroy(): void {
        this.route.queryParamMap.subscribe().unsubscribe();
    }

}