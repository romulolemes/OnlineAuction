import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptorService } from './interceptors/auth-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuctionListComponent } from './component/auction-list/auction-list.component';
import { MaterialModule } from './material-module';
import {
  HttpClientModule,
  HTTP_INTERCEPTORS,
  JsonpInterceptor,
} from '@angular/common/http';
import { AuthGuardService } from './service/auth-guard.service';
import { AuthService } from './service/auth.service';

@NgModule({
  declarations: [AppComponent, AuctionListComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
  ],
  providers: [
    AuthGuardService,
    AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
