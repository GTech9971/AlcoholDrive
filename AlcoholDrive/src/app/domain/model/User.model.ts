export interface UserModel {
    /**
     * ユーザ名
     */
    UserName: string;

    /**
     * ボス
     */
    UserBoss: UserModel;

    /**
     * ユーザ画像パス
     */
    UserImagePath: string;
}