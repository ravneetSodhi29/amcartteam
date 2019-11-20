import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginCallbackComponent } from './login-callback/login-callback.component';
import { LogoutCallbackComponent } from './logout-callback/logout-callback.component';
import { SilentCallbackComponent } from './silent-callback/silent-callback.component';

@NgModule({
  declarations: [LoginCallbackComponent, LogoutCallbackComponent, SilentCallbackComponent],
  imports: [
    CommonModule
  ]
})
export class AuthModule { }
