import { Component, Injectable } from "@angular/core";
import { FormArray, FormGroup } from "@angular/forms";
import { MatDialog } from "@angular/material/dialog";
import { CanDeactivate } from "@angular/router";
import { CandidateProfileComponent } from "../candidate-profile/candidate-profile.component";

@Injectable({
  providedIn: 'root',
})
export class CanDeactivateGuard implements CanDeactivate<CandidateProfileComponent> {
  constructor(private dialog: MatDialog){
  }
  async canDeactivate(component: CandidateProfileComponent): Promise<boolean> {
   if(component.profileForm.dirty ||
    ((component.profileForm.get('siblings') as FormArray).controls as FormGroup[]).some(ctr => ctr.dirty)){
      return await this.dialog.open(DialogElementsExampleDialog).afterClosed().toPromise() ? true : false;
    }
    return true;
  }
}

@Component({
  selector: 'unsaved-dialog',
  template: `<h1 mat-dialog-title>Siva Marriages</h1>
  <mat-dialog-content class="mat-typography">
    <p>You have Unsaved Changes. Do you want to close?</p>
  </mat-dialog-content>
  <mat-dialog-actions align="end">
    <button mat-button [mat-dialog-close]="true">Yes</button>
    <button mat-button [mat-dialog-close]="false">No</button>
  </mat-dialog-actions>`,
})
export class DialogElementsExampleDialog {}
