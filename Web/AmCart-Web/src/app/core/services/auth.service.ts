import { Injectable } from '@angular/core';
import { UserManager, User, Log, SignoutResponse } from 'oidc-client';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userManager: UserManager;
  user: User;

  constructor() {
    Log.logger = console;
    var settings = {
      authority: 'http://localhost:5000',
      client_id: 'amcart.login',
      redirect_uri: 'http://localhost:4200/login-callback',
      post_logout_redirect_uri: 'http://localhost:4200/logout-callback',
      response_type: 'id_token token',
      scope: 'openid profile',
      automaticSilentRenew: true,
      silent_redirect_uri: 'http://localhost:4200/silent-callback'
    };
    this.userManager = new UserManager(settings);

    this.userManager.getUser().then(user => {
      if (user && !user.expired) {
        this.user = user;
        //this.customerService.loadCustomerContext();
      }
    });

    this.userManager.events.addUserLoaded(() => {
      this.userManager.getUser().then(user => {
        this.user = user;
        //this.customerService.loadCustomerContext();
      });
    });
  }

  redirectToLogin(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  loginCallback(): Promise<User> {
    return this.userManager.signinRedirectCallback();
  }

  silentCallback(): Promise<any> {
    return this.userManager.signinSilentCallback();
  }

  redirectToLogout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

  logoutCallback(): Promise<SignoutResponse> {
    return this.userManager.signoutRedirectCallback();
  }

  refreshToken(): Promise<User> {
    return this.userManager.signinSilent();
  }

  getUserDetails(): Promise<User> {
    return this.userManager.getUser();
  }

  isLoggedIn(): boolean {
    return this.user && this.user.access_token && !this.user.expired;
  }

  getAccessToken(): string {
    return this.user ? this.user.access_token : '';
  }
}
