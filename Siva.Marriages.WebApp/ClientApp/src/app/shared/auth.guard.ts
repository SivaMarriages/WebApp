import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

import { AuthService, routesConstants } from './';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  public routers: typeof routesConstants = routesConstants;

  constructor(private service: AuthService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.service.isAthenticated) {
      return true;
    } else {
      this.router.navigate([this.routers.LOGIN], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }
}
