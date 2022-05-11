import { Component, OnInit } from "@angular/core";
import { Observable } from "rxjs";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { AlcDriveState } from "src/app/domain/model/AlcDriveState.model";
import { AlcDriveService } from "src/app/domain/service/AlcDrive.service";

@Component({
    selector: 'alc-check-start',
    templateUrl: './alc-check-start.component.html',
    styleUrls: ['./alc-check-start.component.scss']
})
export class AlcCheckStartComponent implements OnInit {

    state: AlcDriveState;
    private alcDriveStateObserver: Observable<AlcDriveState>;
    alcResult: AlcDriveResultodel;
    private alcDriveResultObserver: Observable<AlcDriveResultodel>;

    constructor(private alcDriveService: AlcDriveService) {
        this.alcDriveStateObserver = this.alcDriveService.AlcDriveStateObserver;
        this.alcDriveResultObserver = this.alcDriveService.AlcDriveResultObserver;

        // 購読
        this.alcDriveStateObserver.subscribe(state => {
            this.state = state;
            console.log(this.state);
        });
        this.alcDriveResultObserver.subscribe(result => {
            this.alcResult = result;
            console.log(this.state);
        });
    }

    async ngOnInit() {
        await this.alcDriveService.startScanning();
    }

}