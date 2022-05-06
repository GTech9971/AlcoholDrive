import { Injectable } from "@angular/core";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { AlcDriveState } from "src/app/domain/model/AlcDriveState.model";
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

    }

    async stopScanning(): Promise<void> {
    }

    async fetchAlcDriveResult(): Promise<void> {
        throw new Error("Method not implemented.");
    }

}