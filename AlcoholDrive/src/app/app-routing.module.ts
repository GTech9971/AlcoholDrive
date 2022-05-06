import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';
import { AlcoholCheckPage } from './presentation/alcohol-check/alcohol-check.page';

const routes: Routes = [
  {
    path: 'connect',
    loadChildren: () => import('./presentation/connect-device/connect-device.module').then(m => m.ConnectDeviceModule)
  },
  {
    path: '',
    redirectTo: 'connect',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: () => import('./presentation/home/home.module').then(m => m.HomePageModule)
  },
  {
    path: 'check',
    component: AlcoholCheckPage
  }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
