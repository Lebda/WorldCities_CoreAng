import { Component, OnInit } from "@angular/core";
import {
  AbstractControl,
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { City } from "../city/city";
import { Country } from "../country/country";
import { BaseFormComponent } from "../Infrastructure/base. form.component";
import { CityService } from "../Infrastructure/city.service";
import { CountryService } from "../Infrastructure/country.service";

@Component({
  selector: "app-city-edit",
  templateUrl: "./city-edit.component.html",
  styleUrls: ["./city-edit.component.css"],
})
export class CityEditComponent extends BaseFormComponent implements OnInit {
  // the view title
  public title = "";
  // the city object to edit
  public item: City | undefined;
  // the city object id, as fetched from the active route:
  // It's NULL when we're adding a new city,
  // and not NULL when we're editing an existing one.
  public id: string | null = null;

  // the countries array for the select
  public countries = new Array<Country>();

  public constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private readonly cityService: CityService,
    private readonly countryService: CountryService
  ) {
    super();
  }

  protected formCreator(): FormGroup {
    return new FormGroup(
      {
        name: new FormControl("", Validators.required),
        lat: new FormControl("", [
          Validators.required,
          Validators.pattern(/^[-]?[0-9]+(\.[0-9]{1,4})?$/),
        ]),
        lon: new FormControl("", [
          Validators.required,
          Validators.pattern(/^[-]?[0-9]+(\.[0-9]{1,4})?$/),
        ]),
        countryId: new FormControl("", Validators.required),
      },
      null,
      this.isDupeCity()
    );
  }

  public ngOnInit() {
    this.loadData();
  }

  public onSubmit(): void {
    const city = this.id && this.item ? this.item : <City>{};

    city.name = this.form.get("name")?.value ?? "";
    city.lat = this.form.get("lat")?.value ?? 0.0;
    city.lon = this.form.get("lon")?.value ?? 0.0;
    city.countryId = this.form.get("countryId")?.value ?? 41193;

    if (this.id) {
      // EDIT mode
      this.cityService.put(city).subscribe(
        (result) => {
          console.log("City " + city.id + " has been updated.");
          // go back to cities view
          this.router.navigate(["/cities"]);
        },
        (error) => console.error(error)
      );
    } else {
      // ADD NEW mode
      this.cityService.post(city).subscribe(
        (result) => {
          console.log("City " + result.id + " has been created.");
          // go back to cities view
          this.router.navigate(["/cities"]);
        },
        (error) => console.error(error)
      );
    }
  }

  private loadData(): void {
    // load countries
    this.loadCountries();

    // retrieve the ID from the 'id'
    this.id = this.activatedRoute.snapshot.paramMap.get("id");
    // fetch the city from the server
    if (this.id) {
      // EDIT MODE
      // fetch the city from the server
      this.cityService.get(this.id).subscribe(
        (result) => {
          this.item = result;
          this.title = "Edit - " + this.item.name;
          // update the form with the city value
          this.form.patchValue(this.item);
        },
        (error) => console.error(error)
      );
    } else {
      // ADD NEW MODE
      this.title = "Create a new City";
    }
  }
  public loadCountries() {
    this.countryService.getData(0, 9999, "name", "asc", "name", null).subscribe(
      (result) => {
        this.countries = result.data;
      },
      (error) => console.error(error)
    );
  }

  private isDupeCity(): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
      const city = <City>{};
      const idNumber = Number(this.id);
      city.id = idNumber ? idNumber : 0;
      city.name = this.form.get("name")?.value;
      city.lat = +this.form.get("lat")?.value;
      city.lon = +this.form.get("lon")?.value;
      city.countryId = this.form.get("countryId")?.value;
      return this.cityService.isDupeCity(city).pipe(
        map((result) => {
          return result ? { isDupeCity: true } : null;
        })
      );
    };
  }
}
