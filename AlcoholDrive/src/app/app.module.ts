import { CUSTOM_ELEMENTS_SCHEMA, NgModule, Provider } from '@angular/core';
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
import { MockAlcDriveRepository } from './infra/AlcDriveRepository/MockAlcDrive.repository';
import { AlcDriveImplRepository } from './infra/AlcDriveRepository/AlcDriveImpl.repository';
import { UserImplRepository } from './infra/UserRepository/UseImpl.repository';
import { NotificationRepository } from './domain/repositories/NotificationRepository/Notification.repository';
import { NotificationImplRepository } from './infra/NotificationRepository/NotificationImpl.repository';
import { MockNotificationRepository } from './infra/NotificationRepository/MockNotification.repository';

// デバック
const debug: Provider[] = [
  { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
  { provide: UserRepository, useClass: MockUserRepository },
  { provide: AlcDriveRepository, useClass: MockAlcDriveRepository },
  { provide: NotificationRepository, useClass: MockNotificationRepository },
]


const build: Provider[] = [
  { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
  { provide: UserRepository, useClass: UserImplRepository },
  { provide: AlcDriveRepository, useClass: AlcDriveImplRepository },
  { provide: NotificationRepository, useClass: NotificationImplRepository },
]

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
  providers: build,
  bootstrap: [AppComponent],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule { }
