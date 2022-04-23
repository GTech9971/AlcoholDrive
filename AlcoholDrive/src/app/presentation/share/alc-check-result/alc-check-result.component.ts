import { Component, Input } from "@angular/core";
import { AlcDriveResultodel } from "src/app/domain/model/AlcDriveResult.model";
import { UserModel } from "src/app/domain/model/User.model";

@Component({
    selector: 'alc-check-result',
    templateUrl: './alc-check-result.component.html',
    styleUrls: ['./alc-check-result.component.scss']
})
export class AlcCheckResultComponent {

    @Input() user: UserModel;

    @Input() alcDriveResult: AlcDriveResultodel;

}