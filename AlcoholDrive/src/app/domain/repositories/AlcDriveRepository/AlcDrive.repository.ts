
export abstract class AlcDriveRepository {
    /** デバイスに接続 */
    abstract connectDevice(): Promise<void>;
    /** デバイスを切断 */
    abstract disconnectDevice(): Promise<void>;

    /** センサーの状態取得 */
    abstract IsConnectDevice(): Promise<void>;

    /** センサーのスキャン開始 */
    abstract startScanning(): Promise<void>;

    /** センサーのスキャン停止 */
    abstract stopScanning(): Promise<void>;

    /** スキャン結果取得 */
    abstract fetchAlcDriveResult(): Promise<void>
}