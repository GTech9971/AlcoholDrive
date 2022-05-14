import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { MessageModel } from "../model/Message.model";

@Injectable({
    providedIn: 'root'
})
export class MessageDeliveryService {

    private readonly _messageSubject: Subject<MessageModel>;

    /**
     * クライアントからのメッセージを監視する
     */
    public readonly MessageObserver: Observable<MessageModel>;

    constructor() {
        this._messageSubject = new Subject<MessageModel>();
        this.MessageObserver = this._messageSubject.asObservable();

        // クライアントからのメッセージを受信する
        window?.chrome?.webview?.addEventListener('message', event => {
            let message: string = event.data + '';
            let cmd: number = Number.parseInt(message.split("=")[0]);
            let jsonStr: string = message.split("=")[1];

            const model: MessageModel = {
                Command: cmd,
                JsonStr: jsonStr
            };
            this._messageSubject.next(model);
            console.log(`Cmd:${model.Command} Data:${model.JsonStr}`);
        });
    }

    /**
     * クライアントに向けてメッセージを送信する
     * @param cmd コマンド
     * @param jsonStr json文字列
     */
    public postMessage(cmd: number, jsonStr: string) {
        window?.chrome?.webview?.postMessage(`${cmd}=${jsonStr}`);
        console.log(`Cmd:${cmd} Data:${jsonStr}`);
    }

    /**
     * クライアントから受信したことにする
     * TEST用
     * @param cmd 
     * @param jsonStr 
     */
    public testRecievedMessage(cmd: number, jsonStr: string) {
        const message: MessageModel = {
            Command: cmd,
            JsonStr: jsonStr
        };
        this._messageSubject.next(message);
    }

}

declare global {
    interface Window {
        chrome: any;
    }
}