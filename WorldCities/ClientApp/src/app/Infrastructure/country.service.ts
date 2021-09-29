import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { Country } from "../country/country";
import { ApiResult } from "../models/api-result";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: "root",
})
export class CountryService extends BaseService<Country> {
  public constructor(http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    super(http, baseUrl);
  }

  public getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string | null
  ): Observable<ApiResult<Country>> {
    const url = this.baseUrl + "api/Countries";
    let params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);

    if (filterQuery) {
      params = params.set("filterColumn", filterColumn).set("filterQuery", filterQuery);
    }
    return this.http.get<ApiResult<Country>>(url, { params });
  }

  public get(id: string): Observable<Country> {
    const url = this.baseUrl + "api/Countries/" + id;
    return this.http.get<Country>(url);
  }

  public put(item: Country): Observable<Country> {
    const idStr = item.id;
    const url = this.baseUrl + "api/Countries/" + idStr;
    return this.http.put<Country>(url, item);
  }

  public post(item: Country): Observable<Country> {
    const url = this.baseUrl + "api/Countries";
    return this.http.post<Country>(url, item);
  }

  public isDupeField(
    countryId: string,
    fieldName: string,
    fieldValue: string
  ): Observable<boolean> {
    const params = new HttpParams()
      .set("countryId", countryId)
      .set("fieldName", fieldName)
      .set("fieldValue", fieldValue);
    const url = this.baseUrl + "api/Countries/IsDupeField";
    return this.http.post<boolean>(url, null, { params });
  }
}
