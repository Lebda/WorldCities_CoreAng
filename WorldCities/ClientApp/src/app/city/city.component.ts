import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { Observable, of } from "rxjs";

import { City } from "./city";

@Component({
  selector: "app-city",
  templateUrl: "./city.component.html",
  styleUrls: ["./city.component.css"],
})
export class CityComponent implements OnInit {
  public citiesSource = new MatTableDataSource<City>();
  @ViewChild(MatPaginator)
  public paginator: MatPaginator | null = null;

  public displayedColumns: string[] = ["id", "name", "lat", "lon"];
  public cities$: Observable<City[]>;

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string) {
    this.cities$ = of(new Array<City>());
  }

  public ngOnInit() {
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = 0;
    pageEvent.pageSize = 10;
    // this.getData(pageEvent);

    this.cities$ = this.http.get<City[]>("https://localhost:44355/" + "api/Cities");
    this.cities$.subscribe(
      (result) => {
        this.citiesSource = new MatTableDataSource<City>();
        this.citiesSource.data = result;
        this.citiesSource.paginator = this.paginator;
      },
      (error) => {
        this.citiesSource = new MatTableDataSource<City>();
        console.error(error);
      }
    );
  }

  // private getData(event: PageEvent): Observable<City[]> {
  //   const url = this.baseUrl + "api/Cities";
  //   const params = new HttpParams()
  //     .set("pageIndex", event.pageIndex.toString())
  //     .set("pageSize", event.pageSize.toString());
  //   this.http.get<any>(url, { params }).subscribe(
  //     (result) => {},
  //     (error) => console.error(error)
  //   );
  // }

  private createParams(event: PageEvent): HttpParams {
    const params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString());

    return params;
  }
}
