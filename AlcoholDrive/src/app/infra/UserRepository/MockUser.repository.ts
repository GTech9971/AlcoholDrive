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

    fetchUsers(): Promise<UserModel[]> {
        return this.client.get<UserModel[]>("./assets/mock/user/data.json").toPromise();
    }

    async fetchUser(userId: number): Promise<UserModel> {
        const users: UserModel[] = await this.fetchUsers();
        return users.filter(u => u.UserId === userId)[0];
    }

}