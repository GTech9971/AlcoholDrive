import { Injectable } from "@angular/core";
import { UserModel } from "../model/User.model";
import { UserRepository } from "../repositories/UserRepository/User.repository";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(private repository: UserRepository) {
    }

    public async fetchUsers(): Promise<UserModel[]> {
        return this.repository.fetchUsers();
    }

    public async getUser(userId: number): Promise<UserModel> {
        return this.repository.fetchUser(userId);
    }
}