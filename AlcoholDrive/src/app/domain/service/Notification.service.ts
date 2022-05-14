import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { NotificationCommands } from "../model/commands/NitificationCommands.model";
import { SendAlcResultModel } from "../model/SendAlcResult.model";
import { NotificationRepository } from "../repositories/NotificationRepository/Notification.repository";
import { MessageDeliveryService } from "./MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class NotificationService {

    private _slackAPI: string;
    private readonly _slackAPISubject: BehaviorSubject<string>;
    public readonly SlackAPIObserver: Observable<string>;

    /**
     * SlackAPIが有効かどうか
     * TODO:空文字かどうかしか見てない
     */
    public get IsEnableSlack(): boolean {
        return this._slackAPI !== '' && this._slackAPI !== undefined;
    }

    constructor(private repository: NotificationRepository,
        private deliveryService: MessageDeliveryService,) {

        this._slackAPI = "";
        this._slackAPISubject = new BehaviorSubject<string>(this._slackAPI);
        this.SlackAPIObserver = this._slackAPISubject.asObservable();

        // 購読
        this.deliveryService.MessageObserver.subscribe(message => {
            // SlackAPI受信
            if (message.Command === NotificationCommands.GET_SLACK_API_RES) {
                this._slackAPI = message.JsonStr;
                this.nextSlackAPI();
            }
        });
    }

    async fetchSlackAPI(): Promise<void> {
        await this.repository.fetchSlackAPI();
    }

    async registrySalckAPI(slackAPI: string): Promise<void> {
        await this.repository.registrySalckAPI(slackAPI);
    }

    async sendSlack(sendAlcResultModel: SendAlcResultModel): Promise<void> {
        await this.repository.sendSlack(sendAlcResultModel);
    }

    private nextSlackAPI() {
        this._slackAPISubject.next(this._slackAPI);
    }
}