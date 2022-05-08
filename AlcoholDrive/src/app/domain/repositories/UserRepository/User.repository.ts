import { Injectable } from "@angular/core";
import { UserModel } from "../../model/User.model";

@Injectable({
    providedIn: 'root'
})
export abstract class UserRepository {
    /**ユーザを取得 */
    abstract getUsers(): Promise<void>
    /**ユーザを登録 */
    abstract registryUser(user: UserModel): Promise<void>;
    /**ユーザを削除 */
    abstract deleteUser(userId: number): Promise<void>;

    /** 上長を設定する */
    abstract setUserBoss(userId: number, bossId: number): Promise<void>;
}