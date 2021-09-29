import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AuthorizeGuard } from "src/api-authorization/authorize.guard";

import { CityEditComponent } from "./city-edit/city-edit.component";
import { CityComponent } from "./city/city.component";
import { CountryEditComponent } from "./country-edit/country-edit.component";
import { CountryComponent } from "./country/country.component";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  { path: "cities", component: CityComponent },
  { path: "city/:id", component: CityEditComponent, canActivate: [AuthorizeGuard] },
  { path: "city", component: CityEditComponent, canActivate: [AuthorizeGuard] },
  { path: "countries", component: CountryComponent },
  { path: "country/:id", component: CountryEditComponent, canActivate: [AuthorizeGuard] },
  { path: "country", component: CountryEditComponent, canActivate: [AuthorizeGuard] },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
