import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { IonicModule } from "@ionic/angular";
import { AlcCheckResultComponentModule } from "../share/alc-check-result/alc-check-result.component.module";
import { AlcCheckStartComponentModule } from "../share/alc-check-start/alc-check-start.component.module";
import { HeaderComponentModule } from "../share/header/header.component.module";
import { AlcoholCheckPageRoutingModule } from "./alcohol-check-routing.module";
import { AlcoholCheckPage } from "./alcohol-check.page";

@NgModule({
    imports: [
        CommonModule,
        IonicModule,
        AlcoholCheckPageRoutingModule,
        AlcCheckResultComponentModule,
        AlcCheckStartComponentModule,
        HeaderComponentModule,
    ], declarations: [AlcoholCheckPage],
})
export class AlcoholCheckModule { }