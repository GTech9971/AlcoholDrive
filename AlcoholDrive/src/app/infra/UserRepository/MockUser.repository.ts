import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserCommands } from "src/app/domain/model/commands/UserCommands.model";
import { UserModel } from "src/app/domain/model/User.model";
import { UserRepository } from "src/app/domain/repositories/UserRepository/User.repository";
import { MessageDeliveryService } from "src/app/domain/service/MessageDelivery.service";

@Injectable({
    providedIn: 'root'
})
export class MockUserRepository extends UserRepository {

    private _users: UserModel[];

    constructor(private client: HttpClient,
        private deliveryService: MessageDeliveryService) {
        super();
        this._users = [];
    }

    async getUsers(): Promise<void> {
        if (this._users.length === 0) {
            this._users = await this.client.get<UserModel[]>('./assets/mock/user/data.json').toPromise();
        }

        this.deliveryService.testRecievedMessage(UserCommands.GET_USERS_RES, JSON.stringify(this._users));
    }

    async registryUser(user: UserModel): Promise<void> {
        this._users.push(user);
    }

    async deleteUser(userId: number): Promise<void> {
        this._users = this._users.filter(u => u.UserId !== userId);
    }

    async setUserBoss(userId: number, bossId: number): Promise<void> {
        const BOSS: UserModel = this._users.find(u => u.UserId === bossId);
        this._users.forEach(u => {
            if (u.UserId === userId) {
                u.UserBoss = BOSS;
            }
        });
    }

}