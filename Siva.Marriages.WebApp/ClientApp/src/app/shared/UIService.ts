import { Injectable } from '@angular/core';
import { Overlay } from '@angular/cdk/overlay';
import { ComponentPortal } from '@angular/cdk/portal';
import { MatSpinner } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';


@Injectable({
  providedIn: 'root',
})
export class UIService {

  private spinnerTopRef = this.cdkSpinnerCreate();
  private spinnerCount:number = 0;

  constructor(private overlay: Overlay, private snackBar: MatSnackBar) {
  }

  private cdkSpinnerCreate() {
    return this.overlay.create({
      hasBackdrop: true,
      backdropClass: 'dark-backdrop',
      positionStrategy: this.overlay.position()
        .global()
        .centerHorizontally()
        .centerVertically()
    })
  }

  public showSpinner() {
    this.spinnerCount++;
    if (!this.spinnerTopRef.hasAttached()) {
      this.spinnerTopRef.attach(new ComponentPortal(MatSpinner));
    }
  }

  public stopSpinner() {
    this.spinnerCount--;
    if (this.spinnerTopRef.hasAttached() && this.spinnerCount === 0) {
      this.spinnerTopRef.detach();
    }
  }

  public showToast(message: string) {
    this.snackBar.open(message, undefined, {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      politeness: 'assertive',
      duration: 5 * 1000
    });
  }
  public showErrorToast(messageObj: any) {
    const message = JSON.stringify(messageObj);
    this.snackBar.open(message, undefined, {
      horizontalPosition: 'center',
      verticalPosition: 'top',
      politeness: 'assertive',
      duration: 5 * 1000
    });
  }
}
