import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService {
  constructor(private authService: AuthService) {}
  async canActivate() {
    const loggedin = await this.authService.isUserLogged();
    if (!loggedin) {
      this.authService.login();
    }
  }
}
