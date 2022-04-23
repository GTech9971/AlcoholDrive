import { Injectable } from "@angular/core";
import { AlcDriveResultodel } from "../model/AlcDriveResult.model";
import { AlcDriveState } from "../model/AlcDriveState.model";
import { AlcDriveRepository } from "../repositories/AlcDriveRepository/AlcDrive.repository";

@Injectable({
    providedIn: 'root'
})
export class AlcDriveService {

    constructor(private repository: AlcDriveRepository) {
    }

    public async fetchSensorState(): Promise<AlcDriveState> {
        return this.repository.fetchSensorState();
    }

    public async startScanning(): Promise<AlcDriveState> {
        return this.repository.startScanning();
    }

    public async fetchAlcDriveResult(): Promise<AlcDriveResultodel> {
        return this.repository.fetchAlcDriveResult();
    }

}