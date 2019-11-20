import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RootComponent } from './root/root.component';
import { HomeComponent } from './home/home.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { LoginCallbackComponent } from './auth/login-callback/login-callback.component';
import { LogoutCallbackComponent } from './auth/logout-callback/logout-callback.component';
import { SilentCallbackComponent } from './auth/silent-callback/silent-callback.component';
import { BlogComponent } from './blog/blog.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {
    path: '',
    component: RootComponent,
    children: [
      {
          path: '',
          component: HomeComponent,
          pathMatch: 'full'
      },
      {
          path: 'register',
          component: RegisterComponent
      },
      {
          path: 'contact',
          component: ContactUsComponent
      },
      {
          path: 'blog',
          component: BlogComponent
      }
    ]
  },
  {
      path: 'login-callback',
      component: LoginCallbackComponent,
      pathMatch: 'full'
  },
  {
      path: 'logout-callback',
      component: LogoutCallbackComponent,
      pathMatch: 'full'
  },
  {
      path: 'silent-callback',
      component: SilentCallbackComponent,
      pathMatch: 'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
