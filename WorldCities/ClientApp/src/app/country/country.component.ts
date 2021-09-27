import { HttpClient, HttpParams } from "@angular/common/http";
import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { MatPaginator, PageEvent } from "@angular/material/paginator";
import { MatSort, SortDirection } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { Observable, Subject, Subscription } from "rxjs";
import { debounceTime, distinctUntilChanged } from "rxjs/operators";

import { ApiResult } from "../models/api-result";
import { Country } from "./country";

@Component({
  selector: "app-country",
  templateUrl: "./country.component.html",
  styleUrls: ["./country.component.css"],
})
export class CountryComponent implements OnInit {
  private subscriptionsCity = new Array<Subscription>();
  public itemsSource = new MatTableDataSource<Country>();
  @ViewChild(MatPaginator)
  public paginator: MatPaginator | null = null;
  @ViewChild(MatSort)
  public sort: MatSort | null = null;

  public displayedColumns: string[] = ["id", "name", "iso2", "iso3", "totCities"];
  public apiResult$: Observable<ApiResult<Country>> | undefined;
  public pageSize = 10;
  public pageIndex = 0;
  public pageLength = 0;
  public defaultSortColumn = "name";
  public defaultSortOrder: SortDirection = "asc";
  private defaultPageIndex = 0;
  private defaultPageSize = 10;
  private defaultFilterColumn = "name";
  private filterQuery: string | null = null;

  private filterTextChanged: Subject<string> = new Subject<string>();

  public constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {
    this.itemsSource.paginator = this.paginator;
  }

  public ngOnInit() {
    this.loadDataInitial();
  }

  public getData(pageEvent: PageEvent): void {
    this.subscriptionsCity.forEach((item) => item.unsubscribe());
    this.subscriptionsCity = new Array<Subscription>();
    this.apiResult$ = this.getDataSource(pageEvent);
    this.subscriptionsCity.push(
      this.apiResult$.subscribe(
        (result) => {
          this.itemsSource.data = result.data;
          this.pageIndex = result.metaData.query.pageIndex;
          this.pageSize = result.metaData.query.pageSize;
          this.pageLength = result.metaData.totalItemsCount;
        },
        (error) => {
          this.itemsSource.data = new Array<Country>();
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

  private getDataSource(event: PageEvent): Observable<ApiResult<Country>> {
    const url = "https://localhost:44355/" + "api/Countries";
    return this.http.get<ApiResult<Country>>(url, { params: this.createParams(event) });
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

  public onFilterTextChanged(filterText: string): void {
    if (this.filterTextChanged.observers.length === 0) {
      this.filterTextChanged
        .pipe(debounceTime(1000), distinctUntilChanged())
        .subscribe((query) => {
          this.loadDataInitial(query);
        });
    }
    this.filterTextChanged.next(filterText);
  }
}
