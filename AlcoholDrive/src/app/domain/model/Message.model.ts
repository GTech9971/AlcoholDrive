/**
 * クライアントから受信したメッセージ
 */
export interface MessageModel {
    /** コマンド */
    Command: number;
    /** Json文字列 */
    JsonStr: string;
}