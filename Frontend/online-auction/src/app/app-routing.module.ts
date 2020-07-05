import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuctionListComponent } from './component/auction-list/auction-list.component';
import { AppComponent } from './app.component';
import { AuthGuardService } from './service/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [AuthGuardService],
  },
  { path: 'list', component: AuctionListComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
