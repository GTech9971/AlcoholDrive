import { UserCommands } from "src/app/domain/model/commands/UserCommands.model";
import { UserModel } from "src/app/domain/model/User.model";
import { UserRepository } from "src/app/domain/repositories/UserRepository/User.repository";
import { MessageDeliveryService } from "src/app/domain/service/MessageDelivery.service";

export class UserImplRepository extends UserRepository {

    constructor(private deliveryService: MessageDeliveryService) {
        super();
    }

    async getUsers(): Promise<void> {
        this.deliveryService.postMessage(UserCommands.GET_USERS, "");
    }

    async registryUser(user: UserModel): Promise<void> {
        const jsonStr: string = JSON.stringify(user);
        this.deliveryService.postMessage(UserCommands.REGISTRY_USER, jsonStr);
    }

    async deleteUser(userId: number): Promise<void> {
        this.deliveryService.postMessage(UserCommands.DEL_USER, userId.toString());
    }

    async setUserBoss(userId: number, bossId: number): Promise<void> {
        this.deliveryService.postMessage(UserCommands.SET_USER_BOSS, `${userId},${bossId}`);
    }

}