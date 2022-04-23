import { AlcDriveResultodel } from "../../model/AlcDriveResult.model";
import { AlcDriveState } from "../../model/AlcDriveState.model";

export abstract class AlcDriveRepository {
    /** センサーの状態取得 */
    abstract fetchSensorState(): Promise<AlcDriveState>;

    /** センサーのスキャン開始 */
    abstract startScanning(): Promise<AlcDriveState>;

    /** スキャン結果取得 */
    abstract fetchAlcDriveResult(): Promise<AlcDriveResultodel>
}