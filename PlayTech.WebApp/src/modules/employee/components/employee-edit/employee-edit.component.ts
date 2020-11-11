import {ChangeDetectionStrategy, Component, Inject, OnDestroy, OnInit, Optional} from '@angular/core';
import {BaseEditComponent} from '../../../../common/components/base-edit.component';
import {EmployeeEdit} from '../../models/employee-edit.model';
import {EmployeesService} from '../../services/employees.service';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';
import {ModeType} from '../../../../common/enums/mode-type.enum';
import {FormBuilder, Validators} from '@angular/forms';
import {KeyValue} from '../../../../common/utils/classes/keyvalue';
import {untilDestroyed} from '@ngneat/until-destroy';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Observable, of} from 'rxjs';
import {catchError, debounceTime, distinctUntilChanged, startWith, switchMap} from 'rxjs/operators';
import {DepartmentService} from '../../../department/services/department.service';
import {FormValidators} from '../../../../common/utils/forms/form-validators';
import {EmployeeHelper} from '../../models/employee.helper';

@Component({
  selector: 'app-employee-edit',
  templateUrl: './employee-edit.component.html',
  styleUrls: ['./employee-edit.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeeEditComponent
  extends BaseEditComponent<EmployeeEdit>
  implements OnInit, OnDestroy {

  get title() {
    return this.mode === ModeType.Create
      ? 'Create Employee'
      : 'Edit Employee';
  }

  managers$: Observable<KeyValue<number, string>[]>;
  departments$: Observable<KeyValue<number, string>[]>;

  constructor(protected readonly dialogRef: MatDialogRef<EmployeeEditComponent>,
              protected readonly formBuilder: FormBuilder,
              @Optional() @Inject(MAT_DIALOG_DATA)
              protected readonly data: number,
              protected readonly employeesService: EmployeesService,
              protected readonly departmentService: DepartmentService,
              protected readonly snackbar: MatSnackBar) {
    super();
  }

  ngOnInit() {
    super.ngOnInit();

    this.managers$ = this.form.controls.manager.valueChanges
      .pipe(untilDestroyed(this), startWith('')
        , debounceTime(300), distinctUntilChanged()
        , switchMap(value => (typeof(value) === 'string' && value.length > 0)
          ? this.employeesService.autocomplete(value).pipe(untilDestroyed(this)
            , catchError((err) => of([]))) : of([])));

    this.departments$ = this.form.controls.department.valueChanges
      .pipe(untilDestroyed(this), startWith('')
        , debounceTime(300), distinctUntilChanged()
        , switchMap(value => (typeof(value) === 'string' && value.length > 0)
          ? this.departmentService.autocomplete(value).pipe(untilDestroyed(this)
            , catchError((err) => of([]))) : of([])));
  }

  displayAutocomplete = (model: KeyValue<number, string>) => {
    return !!model ? model.value : '';
  }

  close(refresh?: boolean) {
    this.dialogRef.close(refresh);
  }

  ngOnDestroy() {
    super.ngOnDestroy();
  }

  protected initIdentifier() {
    return this.data;
  }

  protected initMode() {
    return this.id > 0 ? ModeType.Edit : ModeType.Create;
  }

  protected initForm() {
    return this.formBuilder.group({
      firstName: this.formBuilder.control('', [
        FormValidators.requiredTrim
      ]),
      lastName: this.formBuilder.control('', [
        FormValidators.requiredTrim
      ]),
      salary: this.formBuilder.control('', [
        Validators.required, Validators.min(0)
      ]),
      department: this.formBuilder.control('', [
        FormValidators.autocompleteRequired
      ]),
      manager: this.formBuilder.control('', [
        FormValidators.autocompleteRequired
      ])
    });
  }

  protected refreshForm() {
    this.form.controls.firstName.setValue(EmployeeHelper.getFirstName(this.model));
    this.form.controls.lastName.setValue(EmployeeHelper.getLastName(this.model));
    this.form.controls.salary.setValue(this.model.salary);
    if (this.model.departmentId) {
      this.form.controls.department.setValue({
        key: this.model.departmentId,
        value: this.model.departmentName
      } as KeyValue<number, string>);
    }
    if (this.model.managerId) {
      this.form.controls.manager.setValue({
        key: this.model.managerId,
        value: this.model.managerName
      });
    }
  }

  protected mapModel() {
    this.model.name = `${this.form.controls.firstName.value} ${this.form.controls.lastName.value}`;
    this.model.salary = this.form.controls.salary.value;
    this.model.managerId = this.form.controls.manager?.value?.key;
    this.model.managerName = this.form.controls.manager?.value?.value;
    this.model.departmentId = this.form.controls.department?.value?.key;
    this.model.departmentName = this.form.controls.department?.value?.value;
  }

  protected refreshObservable() {
    return this.employeesService.getById(this.id);
  }

  protected saveUnchangedModel() {
    this.close();
  }

  protected saveModel() {
    super.saveModel();
    this.isLoading$.next(true);
    this.employeesService.save(this.model)
      .pipe(untilDestroyed(this))
      .subscribe((id: number) => {
        this.snackbar.open('Save successful', 'Close');
        this.close(true);
      }, () => {
      });
  }

}
