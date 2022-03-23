import { Component, EventEmitter, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService, UIService } from '../shared';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html'
})
export class LogInComponent {
  @Output() sendLoginForm = new EventEmitter<void>();
  public form: FormGroup;
  public hide = true;

  constructor(private service: AuthService, private route: ActivatedRoute, private router: Router, private uiService: UIService) {
    this.form = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required])
    });
  }

  public async login(): Promise<any> {
    if (this.form.valid) {
      this.service.logIn(this.form.getRawValue())
        .subscribe({
          next: () => {
            // get return url from route parameters or default to '/'
            const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
            this.router.navigate([returnUrl]);
          },
          error: error => {
            this.uiService.showErrorToast(error);
          }
        });
    }
  }
}
