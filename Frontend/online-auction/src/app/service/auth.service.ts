import { User, UserManager, UserManagerSettings } from 'oidc-client';
import { environment } from 'src/environments/environment';

import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';

export { User };

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userManager: UserManager;
  currentUser: User;

  constructor() {
    const settings = {
      authority: environment.stsAuthority,
      client_id: environment.clientId,
      redirect_uri: `${environment.clientRoot}assets/signin-callback.html`,
      silent_redirect_uri: `${environment.clientRoot}assets/silent-callback.html`,
      post_logout_redirect_uri: `${environment.clientRoot}`,
      response_type: 'id_token token',
      scope: environment.clientScope,
    };
    this.userManager = new UserManager(settings);
  }

  public getUser(): Promise<User> {
    return this.userManager.getUser();
  }

  public login(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  public renewToken(): Promise<User> {
    return this.userManager.signinSilent();
  }

  public logout(): Promise<void> {
    return this.userManager.signoutRedirect();
  }

  public async isUserLogged(): Promise<boolean> {
    try {
      const user = await this.getUser();
      let userLogged: boolean = user != null;
      return Promise.resolve(userLogged);
    } catch (err) {
      console.log(err);
      return false;
    }
  }

  isLoggedInObs(): Observable<boolean> {
    return from(this.isUserLogged());
  }
}
