import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { IonicModule } from "@ionic/angular";
import { ConnectDeviceRoutingModule } from "./connect-device-routing.module";
import { ConnectDevicePage } from "./connect-device.page";

@NgModule({
    imports: [
        CommonModule,
        IonicModule,
        ConnectDeviceRoutingModule,
    ], declarations: [ConnectDevicePage],
})
export class ConnectDeviceModule { }