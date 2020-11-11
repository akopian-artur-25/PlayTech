import { OnDestroy, OnInit, Directive } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {IPagedList} from '../interfaces/paged-list.interface';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import {map} from 'rxjs/operators';
import {BaseFilter} from '../models/base-filter.model';
import {PageEvent} from '@angular/material/paginator';

@Directive()
@UntilDestroy()
export abstract class BaseListComponent<TListItem, TFilter extends BaseFilter> implements OnInit, OnDestroy {
  public readonly isLoading$ = new BehaviorSubject<boolean>(false);

  protected dataSource$ = new BehaviorSubject<IPagedList<TListItem>>({} as IPagedList<TListItem>);

  get data$(): Observable<TListItem[]> {
    return this.dataSource$.pipe(untilDestroyed(this)
      , map(source => source.data));
  }

  get totalCount$(): Observable<number> {
    return this.dataSource$.pipe(untilDestroyed(this)
      , map(source => source.totalCount));
  }

  get pageSize$(): Observable<number> {
    return this.dataSource$.pipe(untilDestroyed(this)
      , map(source => source.pageSize));
  }

  get pageIndex$(): Observable<number> {
    return this.dataSource$.pipe(untilDestroyed(this)
      , map(source => source.pageIndex));
  }

  get isNoData$(): Observable<boolean> {
    return this.data$.pipe(untilDestroyed(this)
      , map(source => (!source || source.length <= 0)));
  }

  protected filter$ = new BehaviorSubject<TFilter>({
    pageIndex: BaseFilter.defaultPageIndex,
    pageSize: BaseFilter.defaultPageSize
  } as TFilter);

  constructor() {
  }

  ngOnInit(): void {
    this.refreshData();
  }

  protected abstract refreshDataObservable(filter: TFilter);

  protected refreshData() {
    this.isLoading$.next(true);
    this.refreshDataObservable(this.filter$.getValue()).pipe(untilDestroyed(this))
      .subscribe((pagedList) => {
        this.dataSource$.next(pagedList ? pagedList : {} as IPagedList<TListItem>);
        this.dataLoaded();
        this.isLoading$.next(false);
      }, (error) => {
        this.dataSource$.next({} as IPagedList<TListItem>);
        this.dataLoaded();
        this.isLoading$.next(false);
      });
  }

  protected dataLoaded() {

  }

  pageChange(pageEvent: PageEvent) {
    const newFilter = Object.assign({}, this.filter$.getValue());
    newFilter.pageSize = pageEvent.pageSize;
    newFilter.pageIndex = pageEvent.pageIndex;
    this.filter$.next(newFilter);
    this.refreshData();
  }

  protected setPageSize(pageSize: number) {
    const newFilter = Object.assign({}, this.filter$.getValue());
    newFilter.pageSize = pageSize;
    this.filter$.next(newFilter);
  }

  protected setPageIndex(pageIndex: number) {
    const newFilter = Object.assign({}, this.filter$.getValue());
    newFilter.pageIndex = pageIndex;
    this.filter$.next(newFilter);
  }

  ngOnDestroy(): void {
  }

}
