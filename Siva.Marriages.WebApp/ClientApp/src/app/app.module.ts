import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgxViewerModule } from 'ngx-viewer';

import { AppRoutes } from './shared/routes.constants';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { LogInComponent } from './log-in/log-in.component';
import { CandidateProfileComponent } from './candidate-profile/candidate-profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatModule } from './mat.module';
import { DialogElementsExampleDialog } from './shared/can-deactivate.guard';
import { CandidatePhotosComponent } from './candidate-photos/candidate-photos.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LogInComponent,
    CandidateProfileComponent,
    DialogElementsExampleDialog,
    CandidatePhotosComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(AppRoutes),
    BrowserAnimationsModule,
    MatModule,
    NgxViewerModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
