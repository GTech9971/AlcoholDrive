import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserModel } from 'src/app/domain/model/User.model';
import { UserService } from 'src/app/domain/service/User.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  userList: UserModel[];

  selectedUser: UserModel;

  constructor(private router: Router,
    private userService: UserService) {
    this.userList = [];
    this.selectedUser = undefined;
  }

  async ngOnInit() {
    this.userList = await this.userService.fetchUsers();
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

  isSelected(user: UserModel): boolean {
    return this.selectedUser === user;
  }


  async onClickAlcoholCheck() {
    await this.router.navigate(['check'], { queryParams: { userid: this.selectedUser.UserId } });
  }

}
