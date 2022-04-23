export interface UserModel {
    /** ID */
    UserId: number;
    /** ユーザ名 */
    UserName: string;
    /** ボス */
    UserBoss: UserModel;
    /** ユーザ画像パス */
    UserImagePath: string;
}