import {Component, OnInit, ChangeDetectionStrategy, OnDestroy} from '@angular/core';
import {BaseListComponent} from '../../../../common/components/base-list.component';
import {EmployeeListItem} from '../../models/employee-list-item.model';
import {EmployeeListFilter} from '../../models/employee-list-filter.model';
import {EmployeesService} from '../../services/employees.service';
import {UntilDestroy, untilDestroyed} from '@ngneat/until-destroy';
import {MatSnackBar} from '@angular/material/snack-bar';
import {MatDialog} from '@angular/material/dialog';
import {EmployeeEditComponent} from '../employee-edit/employee-edit.component';
import {pipe} from 'rxjs';
import {EmployeeHelper} from '../../models/employee.helper';

@UntilDestroy()
@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeeListComponent
  extends BaseListComponent<EmployeeListItem, EmployeeListFilter>
  implements OnInit, OnDestroy {

  public readonly displayedColumns = [
    'firstName',
    'lastName',
    'department',
    'manager',
    'salary',
    'buttons'
  ];

  constructor(protected readonly employeeService: EmployeesService,
              protected readonly snackbar: MatSnackBar,
              protected readonly matDialog: MatDialog) {
    super();
  }

  ngOnInit(): void {
    super.ngOnInit();
  }

  protected refreshDataObservable(filter: EmployeeListFilter) {
    return this.employeeService.list(filter)
      .pipe(untilDestroyed(this));
  }
  getFirstName(model: EmployeeListItem) {
    return EmployeeHelper.getFirstName(model);
  }

  getLastName(model: EmployeeListItem) {
    return EmployeeHelper.getLastName(model);
  }

  create() {
    this.matDialog.open(EmployeeEditComponent)
      .afterClosed().pipe(untilDestroyed(this))
      .subscribe((refresh: boolean) => {
        if (refresh) {
          this.refreshData();
        }
      }, () => {

      });
  }

  edit(id: number) {
    this.matDialog.open(EmployeeEditComponent, { data: id})
      .afterClosed().pipe(untilDestroyed(this))
      .subscribe((refresh: boolean) => {
        if (refresh) {
          this.refreshData();
        }
      }, () => {

      });
  }

  delete(id: number) {
    this.isLoading$.next(true);
    this.employeeService.delete(id)
      .pipe(untilDestroyed(this))
      .subscribe(() => {
        this.snackbar.open('Delete successful', 'Close');
        this.refreshData();
        this.isLoading$.next(false);
      }, () => {
        this.isLoading$.next(false);
      });
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

}
