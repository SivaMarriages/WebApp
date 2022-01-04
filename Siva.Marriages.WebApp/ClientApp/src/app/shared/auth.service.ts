import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authenticated: boolean;
  public nameChange: Subject<string> = new Subject<string>();
  public userName: string = '';

  private async setUserName():Promise<void> {
    if (this.authenticated && this.userName === '') {
      this.userName = (await this.http.get<{name:string}>('api/user/getUserName').toPromise()).name;
      this.nameChange.next(this.userName);
    }
  }

  public async isAthenticated(): Promise<boolean> {
    if (this.authenticated) {
      this.setUserName();
      return this.authenticated;
    } else {
      this.authenticated = await this.http.get<boolean>('api/user/IsAuthenticated').toPromise();
      await this.setUserName();
      return this.authenticated;
    }
  }

  constructor(private http: HttpClient) {
    this.authenticated = false;
  }

  public async login(logInForm: any): Promise<void> {
    try {
      await this.http.post('api/user/login', logInForm).toPromise();
      this.authenticated = true;
      return;
    }
    catch (excep) {
      throw excep;
    }
  }

  public async signOut(): Promise<void> {
    this.authenticated = false;
    this.userName = '';
    this.nameChange.next(this.userName);
    await this.http.get('api/user/logout').toPromise();
  }
}
