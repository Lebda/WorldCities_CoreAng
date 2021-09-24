import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import { Observable, Subscription } from "rxjs";

import { ApiResult } from "../models/api-result";
import { City } from "./city";

@Component({
  selector: "app-city",
  templateUrl: "./city.component.html",
  styleUrls: ["./city.component.css"],
})
export class CityComponent implements OnInit {
  private subscriptionsCity = new Array<Subscription>();
  public citiesSource = new MatTableDataSource<City>();
  @ViewChild(MatPaginator)
  public paginator: MatPaginator | null = null;

  public displayedColumns: string[] = ["id", "name", "lat", "lon"];
  public cities$: Observable<ApiResult<City>> | undefined;
  public pageSize = 10;
  public pageIndex = 0;
  public pageLength = 0;

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string) {
    this.citiesSource.paginator = this.paginator;
  }

  public ngOnInit() {
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = 0;
    pageEvent.pageSize = 10;
    this.getData(pageEvent);
  }

  public getData(pageEvent: PageEvent): void {
    this.subscriptionsCity.forEach((item) => item.unsubscribe());
    this.subscriptionsCity = new Array<Subscription>();
    this.cities$ = this.getDataSource(pageEvent);
    this.subscriptionsCity.push(
      this.cities$.subscribe(
        (result) => {
          this.citiesSource.data = result.data;
          this.pageIndex = result.metaData.query.pageIndex;
          this.pageSize = result.metaData.query.pageSize;
          this.pageLength = result.metaData.totalItemsCount;
        },
        (error) => {
          this.citiesSource.data = new Array<City>();
          console.error(error);
        }
      )
    );
  }

  private getDataSource(event: PageEvent): Observable<ApiResult<City>> {
    const url = "https://localhost:44355/" + "api/Cities";
    return this.http.get<ApiResult<City>>(url, { params: this.createParams(event) });
  }

  private createParams(event: PageEvent): HttpParams {
    const params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString());

    return params;
  }
}
