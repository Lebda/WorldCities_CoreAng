import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ApiAuthorizationModule } from "src/api-authorization/api-authorization.module";
import { AuthorizeInterceptor } from "src/api-authorization/authorize.interceptor";

import { AngularMaterialModule } from "./angular-material/angular-material.module";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { CityEditComponent } from "./city-edit/city-edit.component";
import { CityComponent } from "./city/city.component";
import { CountryEditComponent } from "./country-edit/country-edit.component";
import { CountryComponent } from "./country/country.component";
import { HomeComponent } from "./home/home.component";
import { NavMenuComponent } from "./nav-menu/nav-menu.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CityComponent,
    CountryComponent,
    CityEditComponent,
    CountryEditComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizeInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
