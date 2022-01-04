import { Component, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../shared/auth.service';
import { routesConstants } from '../shared/routes.constants';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnDestroy {
  userName:string;
  nameChangeSub:Subscription;
  constructor(private authService:AuthService, private router: Router){
    this.userName = this.authService.userName;
    this.nameChangeSub = this.authService.nameChange.subscribe((value) => {
      this.userName = value;
    });
  }
  ngOnDestroy(): void {
    this.nameChangeSub.unsubscribe();
  }

  async onLogOut():Promise<void>{
    await this.authService.signOut();
    await this.router.navigate([routesConstants.LOGIN]);
  }
}
