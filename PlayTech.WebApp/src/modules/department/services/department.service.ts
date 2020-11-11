import {BaseService} from '../../../common/services/base.service';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {KeyValue} from '../../../common/utils/classes/keyvalue';
import {HttpClient} from '@angular/common/http';
import {HttpErrorHandler} from '../../../common/services/http-error-handler';
import {catchError} from 'rxjs/operators';

@Injectable({ providedIn: 'root'})
export class DepartmentService extends BaseService {
  constructor(protected readonly http: HttpClient,
              protected readonly errorHandler: HttpErrorHandler) {
    super('departments');
  }

  public autocomplete(input: string): Observable<KeyValue<number, string>[]> {
    return this.http.get(`${this.baseUrl}/autocomplete/${input}`)
      .pipe(catchError(this.errorHandler.handleError()));
  }

}
