import { Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { authGuard } from './shared/auth.guard';
import { MainLayoutComponent } from './layouts/main-layout/main-layout.component';

export const routes: Routes = [
  { path: '', redirectTo: '/signin', pathMatch: 'full' },
  {
    path: '', component: UserComponent,
    children: [
      { path: 'signup', component: RegistrationComponent },
      { path: 'signin', component: LoginComponent },
    ]
  },
  {
    path: '', component: MainLayoutComponent, canActivate: [authGuard],
    canActivateChild: [authGuard],
    children: [
      {
        path: 'dashboard', component: DashboardComponent
      }
    ]
  },

];
