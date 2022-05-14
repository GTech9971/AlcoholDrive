import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { IonicModule } from "@ionic/angular";
import { HeaderComponentModule } from "../share/header/header.component.module";
import { ConnectDeviceRoutingModule } from "./connect-device-routing.module";
import { ConnectDevicePage } from "./connect-device.page";

@NgModule({
    imports: [
        CommonModule,
        IonicModule,
        ConnectDeviceRoutingModule,
        HeaderComponentModule,
    ], declarations: [ConnectDevicePage],
})
export class ConnectDeviceModule { }