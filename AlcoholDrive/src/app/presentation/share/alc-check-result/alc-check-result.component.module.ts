import { CommonModule } from "@angular/common";
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { IonicModule } from "@ionic/angular";
import { AlcCheckResultComponent } from "./alc-check-result.component";

@NgModule({
    imports: [
        CommonModule,
        IonicModule,
    ], declarations: [AlcCheckResultComponent],
    exports: [AlcCheckResultComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AlcCheckResultComponentModule { }