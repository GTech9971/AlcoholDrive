import { Injectable } from "@angular/core";
import { UserModel } from "../model/User.model";

@Injectable({
    providedIn: 'root'
})
export class UserService {

    private _selectedUser: UserModel;
    public get SelectedUser(): UserModel {
        return this._selectedUser;
    }

    public set SelectedUser(value: UserModel) {
        if (this._selectedUser === undefined) {
            this._selectedUser = value;
            return;
        }
        if (this._selectedUser === value) {
            this._selectedUser = undefined;
        } else {
            this._selectedUser = value;
        }
    }

    constructor() {
        this._selectedUser = undefined;
    }
}