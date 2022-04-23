import { Injectable } from "@angular/core";
import { UserModel } from "../../model/User.model";

@Injectable({
    providedIn: 'root'
})
export abstract class UserRepository {

    abstract fetchUsers(): Promise<UserModel[]>

    abstract fetchUser(userId: number): Promise<UserModel>;

}