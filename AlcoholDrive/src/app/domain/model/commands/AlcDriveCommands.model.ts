/**
 * クライアントに送信するアルコールセンサーコマンド
 */
export class AlcDriveCommands {
    /** スキャン開始 */
    public static readonly START_SCANNING: number = 300;
    /** スキャン停止 */
    public static readonly STOP_SCANNING: number = 310;
    /** クライアントからのスキャン結果 */
    public static readonly SCAN_RESULT_RES: number = 320;
}