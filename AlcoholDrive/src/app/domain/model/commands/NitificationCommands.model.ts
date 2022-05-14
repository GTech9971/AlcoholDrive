/**
 * 通知系のコマンド
 * クライアントに送信するコマンド
 */
export class NotificationCommands {
    /**SlackのAPIを取得する */
    public static readonly GET_SLACK_API: number = 400;
    /** SlackのAPIを取得する(受信) */
    public static readonly GET_SLACK_API_RES: number = 401;
    /** SlackAPIを登録する */
    public static readonly REGISTRY_SLACK_API: number = 410;
    /** Slackで検査結果を送信する */
    public static readonly SEND_SLACK: number = 420;
}