export interface UserModel {
    /** ID */
    UserId: number;
    /** ユーザ名 */
    UserName: string;
    /** ボスID */
    BossId: number;
    /** ボス */
    UserBoss: UserModel;
    /** ユーザ画像パス */
    UserImagePath: string;
}