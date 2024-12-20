import { Routes } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';

export const routes: Routes = [
    {path:'', component:UserComponent,
        children:[
            {path:'singup', component:RegistrationComponent},
            {path:'singin', component:LoginComponent}
        ]
    },
    {path:'dashboard', component:DashboardComponent}
];
