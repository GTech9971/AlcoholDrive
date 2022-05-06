/**
 * デバイスコマンド
 * クライアントに送信するコマンド
 */
export class DeviceCommands {
    /** デバイス接続 */
    public static readonly CONNECT_DEVICE: number = 200;
    /** デバイス切断 */
    public static readonly DISCONNECT_DEVICE: number = 210;
    /** デバイス接続確認 */
    public static readonly IS_CONNECT_DEVICE: number = 220;
    /** デバイス接続確認 結果 */
    public static readonly IS_CONNECT_DEVICE_RES: number = 221;
}