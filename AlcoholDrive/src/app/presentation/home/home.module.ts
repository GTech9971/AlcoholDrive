import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IonicModule } from '@ionic/angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HomePage } from './home.page';

import { HomePageRoutingModule } from './home-routing.module';
import { UserCardComponentModule } from '../share/user-card/user-card.component.module';
import { HeaderComponentModule } from '../share/header/header.component.module';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    IonicModule,
    HomePageRoutingModule,
    UserCardComponentModule,
    HeaderComponentModule,
  ],
  declarations: [HomePage]
})
export class HomePageModule { }
