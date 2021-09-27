import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import {
  AbstractControl,
  AsyncValidatorFn,
  FormBuilder,
  FormGroup,
  Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";

import { Country } from "../country/country";
import { BaseFormComponent } from "../Infrastructure/base. form.component";

@Component({
  selector: "app-country-edit",
  templateUrl: "./country-edit.component.html",
  styleUrls: ["./country-edit.component.css"],
})
export class CountryEditComponent extends BaseFormComponent implements OnInit {
  // the view title
  public title = "";
  // the city object to edit or create
  public item: Country | undefined;
  // the city object id, as fetched from the active route:
  // It's NULL when we're adding a new country,
  // and not NULL when we're editing an existing one.
  public id: string | null = null;
  public constructor(
    private fb: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {
    super();
  }

  protected formCreator(): FormGroup {
    return this.fb.group({
      name: ["", Validators.required, this.isDupeField("name")],
      iso2: [
        "",
        [Validators.required, Validators.pattern(/^[a-zA-Z]{2}$/)],
        this.isDupeField("iso2"),
      ],
      iso3: [
        "",
        [Validators.required, Validators.pattern(/^[a-zA-Z]{3}$/)],
        this.isDupeField("iso3"),
      ],
    });
  }

  public ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    // retrieve the ID from the 'id'
    this.id = this.activatedRoute.snapshot.paramMap.get("id");
    if (this.id) {
      // EDIT MODE
      // fetch the country from the server
      const url = this.baseUrl + "api/Countries/" + this.id;
      this.http.get<Country>(url).subscribe(
        (result) => {
          this.item = result;
          this.title = "Edit - " + this.item.name;
          // update the form with the country value
          this.form.patchValue(this.item);
        },
        (error) => console.error(error)
      );
    } else {
      // ADD NEW MODE
      this.title = "Create a new Country";
    }
  }

  public onSubmit(): void {
    const country = this.id ? this.item : <Country>{};
    if (!country) {
      return;
    }
    country.name = this.form.get("name")?.value;
    country.iso2 = this.form.get("iso2")?.value;
    country.iso3 = this.form.get("iso3")?.value;
    if (this.id) {
      // EDIT mode
      const url = this.baseUrl + "api/Countries/" + country.id;
      this.http.put<Country>(url, country).subscribe(
        (result) => {
          console.log("Country " + country.id + " has been updated.");
          // go back to cities view
          this.router.navigate(["/countries"]);
        },
        (error) => console.error(error)
      );
    } else {
      // ADD NEW mode
      const url = this.baseUrl + "api/Countries";
      this.http.post<Country>(url, country).subscribe(
        (result) => {
          console.log("Country " + result.id + " has been created.");
          // go back to cities view
          this.router.navigate(["/countries"]);
        },
        (error) => console.error(error)
      );
    }
  }

  private isDupeField(fieldName: string): AsyncValidatorFn {
    return (control: AbstractControl): Observable<{ [key: string]: any } | null> => {
      const params = new HttpParams()
        .set("countryId", this.id ? this.id.toString() : "0")
        .set("fieldName", fieldName)
        .set("fieldValue", control.value);
      const url = this.baseUrl + "api/Countries/IsDupeField";
      return this.http.post<boolean>(url, null, { params }).pipe(
        map((result) => {
          return result ? { isDupeField: true } : null;
        })
      );
    };
  }
}
