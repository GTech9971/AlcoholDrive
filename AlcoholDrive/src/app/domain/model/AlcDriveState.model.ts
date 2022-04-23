export enum AlcDriveState {
    /** 未設定 */
    NONE,
    /** センサー接続中 */
    CONNECTED,
    /** センサー切断 */
    DISCONNECT,
    /** スキャン中 */
    SCANNING,
    /** 失敗 */
    FAIL,
    /** 成功 */
    OK,
}