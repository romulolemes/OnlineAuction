import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { AuthGuardService } from "./core/services/auth-guard.service";
import { PageNotFoundComponent } from "./component/page-not-found/page-not-found.component";
import { AuctionListComponent } from "./component/auction-list/auction-list.component";
import { AuctionAddComponent } from "./component/auction-add/auction-add.component";
// import { modulesRoutes } from "./modules.routes";

export const routes: Routes = [
  {
    path: "",
    component: AppComponent,
    canActivate: [AuthGuardService],
  },
  { path: "list", component: AuctionListComponent },
  { path: "add", component: AuctionAddComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
