import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AppComponent } from "./app.component";
import { AuthGuardService } from "./core/services/auth-guard.service";
import { PageNotFoundComponent } from "./component/page-not-found/page-not-found.component";
// import { modulesRoutes } from "./modules.routes";

export const routes: Routes = [
  {
    path: "",
    component: AppComponent,
    canActivate: [AuthGuardService],
    children: [
      // ...modulesRoutes,
      // {
      //   path: "unauthorized",
      //   component: UnauthorizedComponent
      // },
      { path: "**", component: PageNotFoundComponent },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
