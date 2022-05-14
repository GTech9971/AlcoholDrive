import { Injectable } from "@angular/core";
import { NotificationCommands } from "src/app/domain/model/commands/NitificationCommands.model";
import { SendAlcResultModel } from "src/app/domain/model/SendAlcResult.model";
import { NotificationRepository } from "src/app/domain/repositories/NotificationRepository/Notification.repository";
import { MessageDeliveryService } from "src/app/domain/service/MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class MockNotificationRepository extends NotificationRepository {

    private TEST_API: string = "";

    constructor(private deliveryService: MessageDeliveryService) {
        super();
    }

    async fetchSlackAPI(): Promise<void> {
        this.deliveryService.testRecievedMessage(NotificationCommands.GET_SLACK_API_RES, this.TEST_API);
    }

    async registrySalckAPI(slackAPI: string): Promise<void> {
        this.TEST_API = slackAPI;
    }

    async sendSlack(sendAlcResultModel: SendAlcResultModel): Promise<void> {

    }

}