import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatSort, SortDirection } from "@angular/material/sort";
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
  @ViewChild(MatSort)
  public sort: MatSort | null = null;

  public displayedColumns: string[] = ["id", "name", "lat", "lon"];
  public cities$: Observable<ApiResult<City>> | undefined;
  public pageSize = 10;
  public pageIndex = 0;
  public pageLength = 0;
  public defaultSortColumn = "name";
  public defaultSortOrder: SortDirection = "asc";
  private defaultPageIndex = 0;
  private defaultPageSize = 10;
  private defaultFilterColumn = "name";
  private filterQuery: string | null = null;

  constructor(private http: HttpClient, @Inject("BASE_URL") private baseUrl: string) {
    this.citiesSource.paginator = this.paginator;
  }

  public ngOnInit() {
    this.loadDataInitial();
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

  public loadDataInitial(query: string | null = null): void {
    const pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData(pageEvent);
  }

  private getDataSource(event: PageEvent): Observable<ApiResult<City>> {
    const url = this.baseUrl + "api/Cities";
    return this.http.get<ApiResult<City>>(url, { params: this.createParams(event) });
  }

  private createParams(event: PageEvent): HttpParams {
    let params = new HttpParams()
      .set("pageIndex", event.pageIndex.toString())
      .set("pageSize", event.pageSize.toString())
      .set("sortColumn", this.sort ? this.sort.active : this.defaultSortColumn)
      .set("sortOrder", this.sort ? this.sort.direction : this.defaultSortOrder);
    if (this.filterQuery) {
      params = params
        .set("filterColumn", this.defaultFilterColumn)
        .set("filterQuery", this.filterQuery);
    }
    return params;
  }
}
