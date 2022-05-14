import { Injectable } from "@angular/core";
import { SendAlcResultModel } from "../../model/SendAlcResult.model";

@Injectable({
    providedIn: 'root'
})
export abstract class NotificationRepository {
    /** SlackAPIを取得する */
    abstract fetchSlackAPI(): Promise<void>;
    /** SlackAPIURLを登録する */
    abstract registrySalckAPI(slackAPI: string): Promise<void>;
    /** Slackで検査結果を送信する */
    abstract sendSlack(sendAlcResultModel: SendAlcResultModel): Promise<void>;
}