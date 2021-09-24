import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { CityComponent } from "./city/city.component";
import { CountryComponent } from "./country/country.component";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  { path: "cities", component: CityComponent },
  { path: "countries", component: CountryComponent },
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
