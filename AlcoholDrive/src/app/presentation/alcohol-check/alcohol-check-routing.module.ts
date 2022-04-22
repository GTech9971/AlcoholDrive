import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AlcoholCheckPage } from "./alcohol-check.page";

const routes: Routes = [{
    path: '',
    component: AlcoholCheckPage
}];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AlcoholCheckPageRoutingModule { }