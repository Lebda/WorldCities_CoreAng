import { ResponseMetaData } from "./response-meta-data";

export interface ApiResult<T> {
  readonly metaData: ResponseMetaData;
  readonly data: Array<T>;
}
