import {HttpParams} from '@angular/common/http';
import {deleteNullProperties} from '../utils/null';
import {toUrlParams} from '../utils/url';
import {environment} from '../../environments/environment';

export abstract class BaseService {
  private _baseUrl: string;

  protected constructor(protected readonly controller: string) {
    this.setBaseUrl(controller);
  }

  get baseUrl() {
    return this._baseUrl;
  }

  private setBaseUrl(url: string) {
    this._baseUrl = `${environment.apiEndpoint}/api/${url}`;
  }

  protected toHttpParams(params: object): HttpParams {
    deleteNullProperties(params);
    if (!params) {
      return undefined;
    }
    const urlParams = toUrlParams(params);

    return new HttpParams({ fromString: urlParams });
  }
}
