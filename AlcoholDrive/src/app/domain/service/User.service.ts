import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { UserCommands } from "../model/commands/UserCommands.model";
import { UserModel } from "../model/User.model";
import { UserRepository } from "../repositories/UserRepository/User.repository";
import { MessageDeliveryService } from "./MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private _users: UserModel[];
    private readonly usersSubject: BehaviorSubject<UserModel[]>;
    public readonly UsersObserver: Observable<UserModel[]>;

    constructor(private repository: UserRepository,
        private deliveryService: MessageDeliveryService) {

        this._users = [];
        this.usersSubject = new BehaviorSubject<UserModel[]>(this._users);
        this.UsersObserver = this.usersSubject.asObservable();

        //購読
        this.deliveryService.MessageObserver.subscribe(message => {
            if (message.Command === UserCommands.GET_USERS_RES) {
                const users: UserModel[] = JSON.parse(message.JsonStr);
                this._users = users;
                this.nextUsers();
            }
        });
    }

    public async getUsers(): Promise<void> {
        this.repository.getUsers();
    }

    /**
     * ユーザIDと一致するユーザを取得する
     * @param userId 
     * @returns 
     */
    public getUser(userId: number): UserModel {
        if (this._users === undefined || this._users.length === 0) {
            return undefined;
        }

        return this._users.find(u => u.UserId === userId);
    }

    public async registryUser(user: UserModel): Promise<void> {
        this.repository.registryUser(user);
    }

    public async deleteUser(userId: number): Promise<void> {
        this.repository.deleteUser(userId);
    }

    public async setUserBoss(userId: number, bossId: number): Promise<void> {
        this.repository.setUserBoss(userId, bossId);
    }


    private nextUsers() {
        this.usersSubject.next(this._users);
    }

}