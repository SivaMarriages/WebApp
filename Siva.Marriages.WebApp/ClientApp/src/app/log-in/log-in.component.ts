import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../shared/auth.service';
import {routesConstants} from '../shared/routes.constants';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html'
})
export class LogInComponent {
  @Output() sendLoginForm = new EventEmitter<void>();
  public form: FormGroup;
  public hide = true;

  constructor(private service: AuthService, private router: Router) {
    this.form = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
      remember: new FormControl(false)
    });
  }

  public async login(): Promise<any> {
    try {
      if (this.form.valid) {
        await this.service.login(this.form.getRawValue());
        await this.router.navigate([routesConstants.HOME]);
      }
    }
    catch (excep) {
      await this.router.navigate([routesConstants.LOGIN]);
      throw excep;
    }
  }
}