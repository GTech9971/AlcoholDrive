import { Component, Input } from "@angular/core";
import { UserModel } from "src/app/domain/model/User.model";
import { UserService } from "src/app/domain/service/User.service";

@Component({
    selector: 'alc-user-card',
    templateUrl: './user-card.component.html',
    styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent {

    @Input() user: UserModel;

    constructor(private userService: UserService) {
    }

    getAvatorImagePath(): string {
        if (this.user.UserImagePath) {

        } else {
            return "./assets/avatar.svg"
        }
    }

    isSelected(): boolean {
        return this.userService.SelectedUser === this.user;
    }

    onClickCard() {
        this.userService.SelectedUser = this.user;
    }

}