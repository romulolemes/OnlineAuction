import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { map, catchError, tap } from 'rxjs/operators';
import { Observable, from } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate {
  constructor(private authService: AuthService) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    return from(this.authService.isUserLogged()).pipe(
      tap((loggedin) => {
        if (!loggedin) {
          this.authService.login();
        }
      })
    );
  }
}
