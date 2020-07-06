import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuctionListComponent } from './component/auction-list/auction-list.component';
import { AppComponent } from './app.component';
import { AuthGuardService } from './service/auth-guard.service';
import { AuctionAddEditComponent } from './component/auction-add-edit/auction-add-edit.component';

const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    canActivate: [AuthGuardService],
  },
  { path: 'list', component: AuctionListComponent },
  {
    path: 'add',
    component: AuctionAddEditComponent,
  },
  {
    path: 'edit/:id',
    component: AuctionAddEditComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
