import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService, routesConstants } from '../shared';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  constructor(private authService:AuthService, private router: Router){
  }

  async onLogOut():Promise<void>{
    this.authService.logOut();
    await this.router.navigate([routesConstants.LOGIN]);
  }
}
