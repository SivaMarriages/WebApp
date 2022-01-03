import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Injectable } from '@angular/core';

import { AuthService } from './auth.service';
import { routesConstants } from './routes.constants';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  public routers: typeof routesConstants = routesConstants;

  constructor(private service: AuthService, private router: Router) {
  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    if (await this.service.isAthenticated()) {
      return true;
    } else {
      this.router.navigate([this.routers.LOGIN]);
      return false;
    }
  }
}
