import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { IonicModule } from "@ionic/angular";
import { AlcCheckStartComponent } from "./alc-check-start.component";

@NgModule({
    imports: [
        CommonModule,
        IonicModule,
    ], declarations: [AlcCheckStartComponent],
    exports: [AlcCheckStartComponent]
})
export class AlcCheckStartComponentModule { }