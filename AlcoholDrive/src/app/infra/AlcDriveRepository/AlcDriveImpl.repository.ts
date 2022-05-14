import { Injectable } from "@angular/core";
import { AlcDriveCommands } from "src/app/domain/model/commands/AlcDriveCommands.model";
import { DeviceCommands } from "src/app/domain/model/commands/DeviceCommands.model";
import { AlcDriveRepository } from "src/app/domain/repositories/AlcDriveRepository/AlcDrive.repository";
import { MessageDeliveryService } from "src/app/domain/service/MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class AlcDriveImplRepository extends AlcDriveRepository {

    constructor(private deliveryService: MessageDeliveryService) {
        super();
    }

    async connectDevice(): Promise<void> {
        this.deliveryService.postMessage(DeviceCommands.CONNECT_DEVICE, "");
    }

    async disconnectDevice(): Promise<void> {
        this.deliveryService.postMessage(DeviceCommands.DISCONNECT_DEVICE, "");
    }

    async IsConnectDevice(): Promise<void> {
        this.deliveryService.postMessage(DeviceCommands.IS_CONNECT_DEVICE, "");
    }

    async startScanning(): Promise<void> {
        this.deliveryService.postMessage(AlcDriveCommands.START_SCANNING, "");
    }

    async stopScanning(): Promise<void> {
        this.deliveryService.postMessage(AlcDriveCommands.STOP_SCANNING, "");
    }

    async fetchAlcDriveResult(): Promise<void> {

    }

}