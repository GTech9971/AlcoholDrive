import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserModel } from 'src/app/domain/model/User.model';
import { UserService } from 'src/app/domain/service/User.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage implements OnInit {

  userListObserver: Observable<UserModel[]>;
  userList: UserModel[];

  selectedUser: UserModel;

  constructor(private router: Router,
    private userService: UserService) {
    this.userList = [];
    this.selectedUser = undefined;
    //購読
    this.userListObserver = this.userService.UsersObserver;
    this.userListObserver.subscribe(users => {
      this.userList = users;
    });
  }

  async ngOnInit() {
    await this.userService.getUsers();
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
