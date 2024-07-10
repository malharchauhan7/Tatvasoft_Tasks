import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './MyComponent/user/user.component';
import { AboutComponent } from './MyComponent/about/about.component';
import { RegisterComponent } from './MyComponent/register/register.component';
import { LoginComponent } from './MyComponent/login/login.component';
const routes: Routes = [
  { path: '', component: UserComponent },
  { path: 'about', component: AboutComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
