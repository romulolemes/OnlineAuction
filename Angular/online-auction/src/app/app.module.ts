import { HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { PageNotFoundComponent } from "./component/page-not-found/page-not-found.component";
import { AuctionListComponent } from "./component/auction-list/auction-list.component";
import { AuctionAddComponent } from "./component/auction-add/auction-add.component";
import { MaterialModule } from "./material.module";

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    AuctionListComponent,
    AuctionAddComponent,
  ],
  imports: [MaterialModule, BrowserModule, HttpClientModule, AppRoutingModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
