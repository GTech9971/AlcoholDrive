import { Injectable } from "@angular/core";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { AlcDriveState } from "src/app/domain/model/AlcDriveState.model";
import { AlcDriveRepository } from "src/app/domain/repositories/AlcDriveRepository/AlcDrive.repository";

@Injectable({
    providedIn: 'root'
})
export class MockAlcDriveRepository extends AlcDriveRepository {


    constructor() {
        super();
    }

    async fetchSensorState(): Promise<AlcDriveState> {
        return AlcDriveState.CONNECTED;
    }

    async startScanning(): Promise<AlcDriveState> {
        return AlcDriveState.SCANNING;
    }
    async fetchAlcDriveResult(): Promise<AlcDriveResultodel> {
        return {
            DrivableResult: true,
            State: AlcDriveState.OK,
        }
    }

}