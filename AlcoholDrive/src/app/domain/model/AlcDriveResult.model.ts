import { AlcDriveState } from "./AlcDriveState.model";

export interface AlcDriveResultodel {
    /** センサーの状態 */
    State: AlcDriveState;
    /** true:運転可能, false:運転不可能 */
    DrivableResult: boolean;
    /** 呼気中アルコール濃度 */
    BAC: number;
}