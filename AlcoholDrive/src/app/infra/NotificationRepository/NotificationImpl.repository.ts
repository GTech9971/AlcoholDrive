import { Injectable } from "@angular/core";
import { NotificationCommands } from "src/app/domain/model/commands/NitificationCommands.model";
import { SendAlcResultModel } from "src/app/domain/model/SendAlcResult.model";
import { NotificationRepository } from "src/app/domain/repositories/NotificationRepository/Notification.repository";
import { MessageDeliveryService } from "src/app/domain/service/MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class NotificationImplRepository extends NotificationRepository {

    constructor(private deliveryService: MessageDeliveryService) {
        super();
    }

    async fetchSlackAPI(): Promise<void> {
        this.deliveryService.postMessage(NotificationCommands.GET_SLACK_API, "");
    }

    async registrySalckAPI(slackAPI: string): Promise<void> {
        this.deliveryService.postMessage(NotificationCommands.REGISTRY_SLACK_API, slackAPI);
    }

    async sendSlack(sendAlcResultModel: SendAlcResultModel): Promise<void> {
        const jsonStr: string = JSON.stringify(sendAlcResultModel);
        this.deliveryService.postMessage(NotificationCommands.SEND_SLACK, jsonStr);
    }

}