import { UserModel } from "./User.model";

/**
 * 検査結果の送信
 */
export interface SendAlcResultModel {
    /** true:検査合格, false:検査不合格 */
    AlcCheckResult: boolean;
    /** 呼気中アルコール濃度 */
    BAC: number;
    /** 検査を受けたユーザ */
    User: UserModel;
    /** 今は使用しない */
    Message: string;
}