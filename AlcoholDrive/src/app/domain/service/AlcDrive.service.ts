import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable, Subject } from "rxjs";
import { AlcDriveResultodel } from "../model/AlcDriveResult.model";
import { AlcDriveState } from "../model/AlcDriveState.model";
import { AlcDriveCommands } from "../model/commands/AlcDriveCommands.model";
import { DeviceCommands } from "../model/commands/DeviceCommands.model";
import { AlcDriveRepository } from "../repositories/AlcDriveRepository/AlcDrive.repository";
import { MessageDeliveryService } from "./MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class AlcDriveService {
    /** デバイスの状態 */
    private _alcDriveState: AlcDriveState;
    private readonly alcDriveStateSubject: BehaviorSubject<AlcDriveState>;
    public readonly AlcDriveStateObserver: Observable<AlcDriveState>;

    /**検知結果 */
    private _alcDriveResult: AlcDriveResultodel;
    private readonly alcDriveResultSubject: Subject<AlcDriveResultodel>;
    public readonly AlcDriveResultObserver: Observable<AlcDriveResultodel>;

    constructor(private repository: AlcDriveRepository,
        private deliveryService: MessageDeliveryService) {

        this._alcDriveState = AlcDriveState.DISCONNECT;
        //購読
        this.alcDriveStateSubject = new BehaviorSubject<AlcDriveState>(this._alcDriveState);
        this.AlcDriveStateObserver = this.alcDriveStateSubject.asObservable();

        this.alcDriveResultSubject = new BehaviorSubject<AlcDriveResultodel>(this._alcDriveResult);
        this.AlcDriveResultObserver = this.alcDriveResultSubject.asObservable();

        this.deliveryService.MessageObserver.subscribe(message => {
            // クライアントからのデバイス接続確認の結果
            if (message.Command === DeviceCommands.IS_CONNECT_DEVICE_RES) {
                let isConnect: boolean = message.JsonStr === "true" ? true : false;
                if (isConnect) {
                    this._alcDriveState = AlcDriveState.CONNECTED;
                    this.nextAlcDriveState();
                }
            }

            // 結果受信
            if (message.Command === AlcDriveCommands.SCAN_RESULT_RES) {
                const result: AlcDriveResultodel = JSON.parse(message.JsonStr);
                this._alcDriveResult = result;
                this.nextAlcDriveResult();
            }
        });
    }

    public async ConnectDevice(): Promise<void> {
        this.repository.connectDevice();
        this.IsConnectDevice();
    }

    public async DisconnectDevice(): Promise<void> {
        this.repository.disconnectDevice();
        this.IsConnectDevice();
    }

    public async IsConnectDevice(): Promise<void> {
        this.repository.IsConnectDevice();
    }

    private nextAlcDriveState() {
        this.alcDriveStateSubject.next(this._alcDriveState);
    }


    public async startScanning(): Promise<void> {
        this.repository.startScanning();
    }

    public async stopScanning(): Promise<void> {
        this.repository.stopScanning();
    }


    private nextAlcDriveResult() {
        this.alcDriveResultSubject.next(this._alcDriveResult);
    }

}