import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { ApiResult } from "../models/api-result";

export abstract class BaseService<T> {
  public constructor(protected http: HttpClient, protected baseUrl: string) {}

  public abstract getData(
    pageIndex: number,
    pageSize: number,
    sortColumn: string,
    sortOrder: string,
    filterColumn: string,
    filterQuery: string
  ): Observable<ApiResult<T>>;
  public abstract get(id: string): Observable<T>;
  public abstract put(item: T): Observable<T>;
  public abstract post(item: T): Observable<T>;
}
