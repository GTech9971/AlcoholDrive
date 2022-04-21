import { CommonModule } from "@angular/common";
import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { IonicModule } from "@ionic/angular";
import { UserCardComponent } from "./user-card.component";

@NgModule({
    imports: [
        CommonModule,
        IonicModule
    ], declarations: [UserCardComponent],
    exports: [UserCardComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class UserCardComponentModule { }