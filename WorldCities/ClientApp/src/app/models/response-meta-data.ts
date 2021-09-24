import { QueryMetaDataResponse } from "./query-meta-data";

export interface ResponseMetaData {
  readonly query: QueryMetaDataResponse;
  readonly totalPagesCount: number;
  readonly totalItemsCount: number;
  readonly hasPrevious: boolean;
  readonly hasNext: boolean;
  readonly info: string | null;
}
