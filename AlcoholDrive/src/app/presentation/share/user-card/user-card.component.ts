import { Component, Input } from "@angular/core";
import { AlertController, PopoverController } from "@ionic/angular";
import { UserModel } from "src/app/domain/model/User.model";
import { UserService } from "src/app/domain/service/User.service";

@Component({
    selector: 'alc-user-card',
    templateUrl: './user-card.component.html',
    styleUrls: ['./user-card.component.scss']
})
export class UserCardComponent {

    @Input() user: UserModel;

    constructor(private userService: UserService,
        private alertCtrl: AlertController) { }

    getAvatorImagePath(): string {
        if (this.user.UserImagePath) {

        } else {
            return "./assets/avatar.svg"
        }
    }

    async onClickMenu(event: any) {
    }

    /**
     * ユーザを削除する
     * ユーザを再取得する
     */
    async onClickDeleteUser(popver: any) {
        // const confirm: HTMLIonAlertElement = await this.alertCtrl.create({
        //     header: '確認',
        //     message: `ユーザ:${this.user.UserName}を削除しますか？`,
        //     buttons: [{
        //         text: 'キャンセル',
        //         role: 'cancel',
        //     }, {
        //         text: 'はい',
        //         id: 'confirm-button',
        //         handler: async () => {

        //         }
        //     }]
        // });
        // await confirm.present();

        popver.dismiss();
        await this.userService.deleteUser(this.user.UserId);
        await this.userService.getUsers();

    }

}