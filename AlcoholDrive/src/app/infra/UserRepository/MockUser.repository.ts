import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserModel } from "src/app/domain/model/User.model";
import { UserRepository } from "src/app/domain/repositories/UserRepository/User.repository";

@Injectable({
    providedIn: 'root'
})
export class MockUserRepository extends UserRepository {

    constructor(private client: HttpClient) {
        super();
    }

    getUsers(): Promise<void> {
        throw new Error("Method not implemented.");
    }
    registryUser(user: UserModel): Promise<void> {
        throw new Error("Method not implemented.");
    }
    deleteUser(userId: number): Promise<void> {
        throw new Error("Method not implemented.");
    }
    setUserBoss(userId: number, bossId: number): Promise<void> {
        throw new Error("Method not implemented.");
    }

}