import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertController } from '@ionic/angular';
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

  // ユーザ登録用のフォーム
  formGroup: FormGroup = this.formBuilder.group({
    USER_NAME: ['', Validators.required],
    BOSS_ID: [],
  });

  constructor(private router: Router,
    private formBuilder: FormBuilder,
    private alertCtrl: AlertController,
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

    if (this.selectedUser.UserId === user.UserId) {
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

  /** モーダルを閉じる */
  onClickCloseModal(modal: any) {
    modal.dismiss();
  }


  /** ユーザの登録 */
  async onClickRegistryUserButton(modal: any) {
    if (this.formGroup.invalid) {
      const alert: HTMLIonAlertElement = await this.alertCtrl.create({ message: 'ユーザ名を入力してください。' });
      await alert.present();
      return;
    }

    const USER_NAME: string = this.formGroup.get('USER_NAME').value;
    const BOSS_ID: number = this.formGroup.get('BOSS_ID').value;

    const BOSS: UserModel = this.userList.find(u => u.UserId === BOSS_ID);

    const user: UserModel = {
      UserName: USER_NAME,
      UserId: -1,
      BossId: BOSS?.UserId,
      UserBoss: BOSS,
      UserImagePath: ''
    };

    await this.userService.registryUser(user);
    await this.userService.getUsers();

    this.onClickCloseModal(modal);
    this.formGroup.get('USER_NAME').setValue("");
    this.formGroup.get('BOSS_ID').setValue(undefined);
  }

}
