/**
 * フロントからクライアントに送信するコマンド
 * ユーザ系のコマンド
 */
export class UserCommands {
    /** ユーザを取得 */
    public static readonly GET_USERS: number = 100;
    /** ユーザを取得のレスポンス */
    public static readonly GET_USERS_RES: number = 101;
    /** ユーザを登録 */
    public static readonly REGISTRY_USER: number = 110;
    /** ユーザを削除 */
    public static readonly DEL_USER: number = 120;
    /** 上長を設定 */
    public static readonly SET_USER_BOSS: number = 130;
}