export abstract class BaseFilter {
  static readonly defaultPageIndex = 0;
  static readonly defaultPageSize = 25;

  pageIndex: number;
  pageSize: number;
  protected constructor() {
    this.pageIndex = BaseFilter.defaultPageIndex;
    this.pageSize = BaseFilter.defaultPageSize;
  }
}
