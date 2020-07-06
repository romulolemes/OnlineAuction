import { Injectable } from '@angular/core';
import { User } from 'oidc-client';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
} from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AuthService } from '../service/auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthInterceptorService implements HttpInterceptor {
  constructor(private authService: AuthService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return from(this.authService.getUser()).pipe(
      switchMap((user) => {
        if (user) {
          return this.handleHequest(
            req.clone({
              setHeaders: {
                Authorization: `${user.token_type} ${user.access_token}`,
              },
            }),
            next
          );
        }

        return this.handleHequest(req, next);
      })
    );
  }

  handleHequest(req: HttpRequest<any>, next: HttpHandler) {
    return next.handle(req);
  }
}
