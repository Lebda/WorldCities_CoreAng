export interface QueryMetaData {
  readonly isZeroBase: true;
  readonly pageIndex: number;
  readonly pageSize: number;
}

export interface QueryMetaDataResponse extends QueryMetaData {
  readonly baseIndex: number;
  readonly maxPageSize: number;
}
