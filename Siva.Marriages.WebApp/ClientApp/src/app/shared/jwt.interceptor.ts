import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { AuthService } from './';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  //hostUrl:string = "";
  constructor(private authService: AuthService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // add auth header with jwt if user is logged in and request is to the api url
    const isLoggedIn: boolean = this.authService.isAthenticated;
    if (isLoggedIn) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${this.authService.Token}`
        }
      });
    }
    // if(this.hostUrl !== ""){
    //   request = request.clone({
    //     url: this.hostUrl + request.urlWithParams
    //   });
    // }
    return next.handle(request);
  }
}
