import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouteReuseStrategy } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { IonicModule, IonicRouteStrategy } from '@ionic/angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AlcoholCheckModule } from './presentation/alcohol-check/alcohol-check.module';
import { UserRepository } from './domain/repositories/UserRepository/User.repository';
import { MockUserRepository } from './infra/UserRepository/MockUser.repository';
import { AlcDriveRepository } from './domain/repositories/AlcDriveRepository/AlcDrive.repository';
import { MockAlcDriveRepository } from './infra/UserRepository/MockAlcDrive.repository';
import { AlcDriveImplRepository } from './infra/UserRepository/AlcDriveImpl.repository';

@NgModule({
  declarations: [AppComponent],
  entryComponents: [],
  imports: [
    BrowserModule,
    IonicModule.forRoot(),
    AppRoutingModule,
    AlcoholCheckModule,
    HttpClientModule,
  ],
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: UserRepository, useClass: MockUserRepository },
    { provide: AlcDriveRepository, useClass: AlcDriveImplRepository },
  ],
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule { }
