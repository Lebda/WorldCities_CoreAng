import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { City } from "../city/city";

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
  private id: string | null = null;

  public constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {
    this.form = new FormGroup({
      name: new FormControl(""),
      lat: new FormControl(""),
      lon: new FormControl(""),
    });
  }

  ngOnInit() {
    this.loadData();
  }

  public onSubmit(): void {
    if (!this.item) {
      return;
    }
    this.item.name = this.form.get("name")?.value ?? "";
    this.item.lat = this.form.get("lat")?.value ?? 0.0;
    this.item.lon = this.form.get("lon")?.value ?? 0.0;
    const url = this.getUrl(this.item.id.toString());
    this.http.put<City>(url, this.item).subscribe(
      (result) => {
        if (this.item) {
          console.log("City " + this.item.id + " has been updated.");
        }
        // go back to cities view
        this.router.navigate(["/cities"]);
      },
      (error) => console.error(error)
    );
  }

  private loadData(): void {
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

  private getUrl(id: string | null): string {
    return "https://localhost:44355/" + "api/Cities/" + id;
  }
}
