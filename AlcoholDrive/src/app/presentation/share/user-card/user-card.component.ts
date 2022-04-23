import { Component, Input } from "@angular/core";
import { UserModel } from "src/app/domain/model/User.model";

@Component({
    selector: 'alc-user-card',
    templateUrl: './user-card.component.html',
    styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent {

    @Input() user: UserModel;

    constructor() { }

    getAvatorImagePath(): string {
        if (this.user.UserImagePath) {

        } else {
            return "./assets/avatar.svg"
        }
    }

}