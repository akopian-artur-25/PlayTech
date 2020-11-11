import {Injectable} from '@angular/core';
import {Observable, throwError} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({providedIn: 'root'})
export class HttpErrorHandler {

  constructor(private snackbar: MatSnackBar) {
  }

  handleError(isSilent: boolean = false) {
    return (response: HttpErrorResponse): Observable<any> => {
      let serverErrorMessage = '';

      if (response && response.error) {
        const errors = (response.error as any).errors;

        if(errors && errors.length > 0 && Array.isArray(errors)) {
          let errorMessage = '';

          errorMessage = errors.join('. ');

          if (!isSilent) {
            this.snackbar.open(errorMessage, 'Close');
          }

          return throwError(new HttpErrors(errorMessage, errors));
        }
        else {
          serverErrorMessage = `${response.status}: ${response.message}`;
        }
      } else {
        serverErrorMessage = 'Unknown error';
      }

      if (!isSilent) {
        this.snackbar.open(serverErrorMessage, 'Close');
      }


      return throwError(new HttpErrors(serverErrorMessage));
    };
  }
}

export class HttpErrors {
  constructor(public message: string,
              public errors: Array<string> = [])
  {
  }
}
