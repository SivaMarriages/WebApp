import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authenticated: boolean;

  public async isAthenticated(): Promise<boolean> {
    if(this.authenticated){
      return this.authenticated;
    }else{
      this.authenticated = await this.http.get<boolean>('api/user/IsAuthenticated').toPromise();
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
    await this.http.get('api/user/logout').toPromise();
  }
}
