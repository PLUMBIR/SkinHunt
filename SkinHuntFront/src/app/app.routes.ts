import { Routes } from '@angular/router';
import { BuyPageComponent } from './body/buy-page/buy-page.component';
import { ProfileComponent } from './body/profile/profile.component';
import { authGuard } from './guards/auth.guard';
import { AdminComponent } from './body/admin/admin.component';
import { BasketComponent } from './body/basket/basket.component';
import { HistoryComponent } from './body/history/history.component';

export const BODY_ROUTES: Routes = [
  { 
    path: '',
    pathMatch: 'full',
    component: BuyPageComponent
  },
  { 
    path: 'profile',
    pathMatch: 'full',
    component: ProfileComponent,
    canActivate: [ authGuard() ]
  },
  {
    path: 'users',
    pathMatch: 'full',
    component: AdminComponent,
    canActivate: [ authGuard() ]
  },
  {
    path: 'basket',
    pathMatch: 'full',
    component: BasketComponent,
    canActivate: [ authGuard() ]
  },
  {
    path: 'history',
    pathMatch: 'full',
    component: HistoryComponent,
    canActivate: [ authGuard() ]
  }
]
