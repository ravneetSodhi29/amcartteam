import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { AuthService } from "./services/auth.service";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private authService: AuthService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (!this.authService.isLoggedIn()) {
            sessionStorage.setItem('returnUrl', route.routeConfig.path);
            this.authService.redirectToLogin();
            return false;
        }
        else {
            return true;
        }
    }
}
