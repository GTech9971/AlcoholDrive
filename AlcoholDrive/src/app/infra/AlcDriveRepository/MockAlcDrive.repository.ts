import { Injectable } from "@angular/core";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { AlcDriveState } from "src/app/domain/model/AlcDriveState.model";
import { AlcDriveRepository } from "src/app/domain/repositories/AlcDriveRepository/AlcDrive.repository";

@Injectable({
    providedIn: 'root'
})
export class MockAlcDriveRepository extends AlcDriveRepository {

    connectDevice(): Promise<void> {
        throw new Error("Method not implemented.");
    }
    disconnectDevice(): Promise<void> {
        throw new Error("Method not implemented.");
    }
    IsConnectDevice(): Promise<void> {
        throw new Error("Method not implemented.");
    }
    startScanning(): Promise<void> {
        throw new Error("Method not implemented.");
    }
    stopScanning(): Promise<void> {
        throw new Error("Method not implemented.");
    }
    fetchAlcDriveResult(): Promise<void> {
        throw new Error("Method not implemented.");
    }




}