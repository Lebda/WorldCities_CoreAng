import { HttpClient, HttpParams } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { City } from "../city/city";
import { ApiResult } from "../models/api-result";
import { BaseService } from "./base.service";

@Injectable({
  providedIn: "root",
})
export class CityService extends BaseService<City> {
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
  ): Observable<ApiResult<City>> {
    const url = this.baseUrl + "api/Cities";
    let params = new HttpParams()
      .set("pageIndex", pageIndex.toString())
      .set("pageSize", pageSize.toString())
      .set("sortColumn", sortColumn)
      .set("sortOrder", sortOrder);

    if (filterQuery) {
      params = params.set("filterColumn", filterColumn).set("filterQuery", filterQuery);
    }
    return this.http.get<ApiResult<City>>(url, { params });
  }

  public get(id: string): Observable<City> {
    const url = this.baseUrl + "api/Cities/" + id;
    return this.http.get<City>(url);
  }

  public put(item: City): Observable<City> {
    const idStr = item.id;
    const url = this.baseUrl + "api/Cities/" + idStr;
    return this.http.put<City>(url, item);
  }

  public post(item: City): Observable<City> {
    const url = this.baseUrl + "api/Cities";
    return this.http.post<City>(url, item);
  }

  public isDupeCity(item: City): Observable<boolean> {
    const url = this.baseUrl + "api/Cities/IsDupeCity";
    return this.http.post<boolean>(url, item);
  }
}
