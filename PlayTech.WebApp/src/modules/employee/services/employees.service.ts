import {BaseService} from '../../../common/services/base.service';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {KeyValue} from '../../../common/utils/classes/keyvalue';
import {catchError} from 'rxjs/operators';
import {HttpClient} from '@angular/common/http';
import {HttpErrorHandler} from '../../../common/services/http-error-handler';
import {IPagedList} from '../../../common/interfaces/paged-list.interface';
import {EmployeeListItem} from '../models/employee-list-item.model';
import {EmployeeListFilter} from '../models/employee-list-filter.model';
import {EmployeeEdit} from '../models/employee-edit.model';

@Injectable({ providedIn: 'root' })
export class EmployeesService extends BaseService {
  constructor(protected readonly http: HttpClient,
              protected readonly errorHandler: HttpErrorHandler) {
    super('employees');
  }

  public list(filter: EmployeeListFilter): Observable<IPagedList<EmployeeListItem>> {
    return this.http.get<IPagedList<EmployeeListItem>>(this.baseUrl, { params: this.toHttpParams(filter)})
      .pipe(catchError(this.errorHandler.handleError()));
  }

  public getById(id: number): Observable<EmployeeEdit> {
    return this.http.get<EmployeeEdit>(`${this.baseUrl}/${id}`)
      .pipe(catchError(this.errorHandler.handleError()));
  }

  public save(model: EmployeeEdit): Observable<number> {
    return (model?.id > 0)
      ? this.http.put<number>(`${this.baseUrl}/${model.id}`, model)
        .pipe(catchError(this.errorHandler.handleError()))
      : this.http.post<number>(`${this.baseUrl}`, model)
        .pipe(catchError(this.errorHandler.handleError()));
  }

  public delete(id: number): Observable<number> {
    return this.http.delete(`${this.baseUrl}/${id}`)
      .pipe(catchError(this.errorHandler.handleError()));
  }

  public autocomplete(input: string): Observable<KeyValue<number, string>[]> {
    return this.http.get(`${this.baseUrl}/autocomplete/${input}`)
      .pipe(catchError(this.errorHandler.handleError()));
  }

}
