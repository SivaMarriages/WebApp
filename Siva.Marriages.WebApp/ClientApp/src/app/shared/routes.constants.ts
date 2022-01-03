import { Routes } from '@angular/router/router';

import { AuthGuard } from './auth.guard';
import { HomeComponent } from '../home/home.component';
import { LogInComponent } from '../log-in/log-in.component';
import { CandidateProfileComponent } from '../candidate-profile/candidate-profile.component';
import { CanDeactivateGuard } from './can-deactivate.guard';
import { CandidatePhotosComponent } from '../candidate-photos/candidate-photos.component';

export enum routesConstants {
  HOME = '',
  VIEWPROFILE = 'view-profile',
  EDITPROFILE = 'edit-profile',
  LOGIN = 'login',
  NEWPROFILE = 'new-profile',
  PHOTOS = 'photos'
}


export const AppRoutes: Routes = [
  { path: '', canActivate: [AuthGuard], component: HomeComponent, pathMatch: 'full' },
  { path: routesConstants.LOGIN, component: LogInComponent },
  { path: routesConstants.NEWPROFILE, canActivate: [AuthGuard], component: CandidateProfileComponent },
  { path: routesConstants.VIEWPROFILE + '/:id', canActivate: [AuthGuard], component: CandidateProfileComponent },
  { path: routesConstants.EDITPROFILE + '/:id', canActivate: [AuthGuard], canDeactivate: [CanDeactivateGuard], component: CandidateProfileComponent },
  { path: routesConstants.PHOTOS + '/:id', canActivate: [AuthGuard], component: CandidatePhotosComponent },
];

