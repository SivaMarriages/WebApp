import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private SetToken(token: string) {
    localStorage.setItem('currentToken', token);
  }

  public get Token(): string {
    return localStorage.getItem('currentToken') ?? "";
  }

  public get isAthenticated(): boolean {
    return this.Token !== "";
  }

  constructor(private http: HttpClient) {
  }

  public logIn(logInForm: any): Observable<void> {
    return this.http.post<any>('auth/user/login', logInForm).pipe(map(data => {
      this.SetToken(data.token);
    }));
  }

  public logOut(): void {
    this.SetToken("");
  }
}
