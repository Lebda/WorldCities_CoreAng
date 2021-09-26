import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import {
  AbstractControl,
  AsyncValidatorFn,
  FormControl,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { City } from "../city/city";
import { Country } from "../country/country";
import { map } from "rxjs/operators";

@Component({
  selector: "app-city-edit",
  templateUrl: "./city-edit.component.html",
  styleUrls: ["./city-edit.component.css"],
})
export class CityEditComponent implements OnInit {
  // the view title
  public title = "";
  // the form model
  public form: FormGroup;
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
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {
    this.form = new FormGroup(
      {
        name: new FormControl("", Validators.required),
        lat: new FormControl("", Validators.required),
        lon: new FormControl("", Validators.required),
        countryId: new FormControl("", Validators.required),
      },
      null,
      this.isDupeCity()
    );
  }

  ngOnInit() {
    this.loadData();
  }

  public onSubmit(): void {
    const city = this.id && this.item ? this.item : <City>{};

    city.name = this.form.get("name")?.value ?? "";
    city.lat = this.form.get("lat")?.value ?? 0.0;
    city.lon = this.form.get("lon")?.value ?? 0.0;
    city.countryId = this.form.get("countryId")?.value ?? 41193;

    const url = this.getUrl(this.item?.id?.toString() ?? null);

    if (this.id) {
      // EDIT mode
      this.http.put<City>(url, city).subscribe(
        (result) => {
          console.log("City " + city.id + " has been updated.");
          // go back to cities view
          this.router.navigate(["/cities"]);
        },
        (error) => console.error(error)
      );
    } else {
      // ADD NEW mode
      this.http.post<City>(url, city).subscribe(
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
    const url = this.getUrl(this.id);
    if (this.id) {
      // EDIT MODE
      // fetch the city from the server
      this.http.get<City>(url).subscribe(
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
  loadCountries() {
    // fetch all the countries from the server
    const url = this.baseUrl + "api/Countries/";
    const params = new HttpParams()
      .set("pageIndex", "0")
      .set("pageSize", "9999")
      .set("sortColumn", "name");
    this.http.get<any>(url, { params }).subscribe(
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
      const url = this.baseUrl + "api/Cities/IsDupeCity";
      return this.http.post<boolean>(url, city).pipe(
        map((result) => {
          return result ? { isDupeCity: true } : null;
        })
      );
    };
  }

  private getUrl(id: string | null): string {
    if (id) {
      return this.baseUrl + "api/Cities/" + id;
    }
    return this.baseUrl + "api/Cities/";
  }
}
