import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/domain/model/User.model';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {

  userList: UserModel[];

  selectedUser: UserModel;

  constructor() {
    this.userList = [];
    this.userList.push({ UserName: 'AAA', UserImagePath: '', UserBoss: undefined });
    this.userList.push({ UserName: 'BBB', UserImagePath: '', UserBoss: undefined });
    this.userList.push({ UserName: 'CCC', UserImagePath: '', UserBoss: undefined });

    this.selectedUser = undefined;
  }

  onClickUserCard(user: UserModel) {
    if (this.selectedUser === undefined) {
      this.selectedUser = user;
      return;
    }
    if (this.selectedUser === user) {
      this.selectedUser = undefined;
    } else {
      this.selectedUser = user;
    }
  }

}
