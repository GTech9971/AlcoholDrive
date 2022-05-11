import { Injectable } from "@angular/core";
import { AlcDriveCommands } from "src/app/domain/model/commands/AlcDriveCommands.model";
import { DeviceCommands } from "src/app/domain/model/commands/DeviceCommands.model";
import { AlcDriveRepository } from "src/app/domain/repositories/AlcDriveRepository/AlcDrive.repository";
import { MessageDeliveryService } from "src/app/domain/service/MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class MockAlcDriveRepository extends AlcDriveRepository {

    constructor(private deliveryService: MessageDeliveryService) {
        super();
    }

    async connectDevice(): Promise<void> {
        this.deliveryService.testRecievedMessage(DeviceCommands.IS_CONNECT_DEVICE_RES, "true");
    }

    disconnectDevice(): Promise<void> {
        throw new Error("Method not implemented.");
    }

    async IsConnectDevice(): Promise<void> {
        this.deliveryService.testRecievedMessage(DeviceCommands.IS_CONNECT_DEVICE_RES, "true");
    }

    async startScanning(): Promise<void> {
        this.deliveryService.testRecievedMessage(AlcDriveCommands.START_SCANNING, "");
        this.fetchAlcDriveResult();
    }

    async stopScanning(): Promise<void> {
        this.deliveryService.testRecievedMessage(AlcDriveCommands.STOP_SCANNING, "");
    }

    async fetchAlcDriveResult(): Promise<void> {
        this.deliveryService.testRecievedMessage(AlcDriveCommands.SCAN_RESULT_RES, "true");
    }
}