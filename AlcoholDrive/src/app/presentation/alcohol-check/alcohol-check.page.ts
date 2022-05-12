import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs";
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

    get STATE_SCANNING(): AlcDriveState { return AlcDriveState.SCANNING; }
    get STATE_OK(): AlcDriveState { return AlcDriveState.OK; }

    user: UserModel;

    alcDriveResult: AlcDriveResultodel;
    private alcDriveResultObserver: Observable<AlcDriveResultodel>;

    constructor(private userService: UserService,
        private alcDriveService: AlcDriveService,
        private route: ActivatedRoute) {

        this.alcDriveResultObserver = this.alcDriveService.AlcDriveResultObserver;

        // 購読
        this.alcDriveResultObserver.subscribe(result => {
            this.alcDriveResult = result;
        });
    }


    async ngOnInit() {
        this.route.queryParamMap.subscribe(async params => {
            let id: number = Number.parseInt(params.get('userid'));
            this.user = await this.userService.getUser(id);
        });

        await this.alcDriveService.startScanning();
    }

    ngOnDestroy(): void {
        this.route.queryParamMap.subscribe().unsubscribe();
    }

}