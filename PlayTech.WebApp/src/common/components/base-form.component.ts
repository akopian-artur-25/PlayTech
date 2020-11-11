import {FormGroup} from '@angular/forms';
import { OnDestroy, OnInit, Directive } from '@angular/core';
import {FormHelper} from '../utils/forms/form-helper';
import {BehaviorSubject} from 'rxjs';

@Directive()
export abstract class BaseFormComponent implements OnInit, OnDestroy {
  public readonly isLoading$ = new BehaviorSubject<boolean>(false);

  public form: FormGroup;

  public isFormOnceSubmitted = false;

  get isInvalidFormView() {
    return this.isFormOnceSubmitted && (!this.form.valid || !this.validateForm());
  }

  protected constructor() {
  }

  ngOnInit() {
    this.form = this.initForm();
  }

  protected initForm() {
    return new FormGroup({});
  }

  protected validateForm() {
    return true;
  }

  submit() {
    this.isFormOnceSubmitted = true;
    FormHelper.validateAllFields(this.form);
    if(this.form.valid && this.validateForm()) {
      this.save();
    }
  }

  protected save() {
  }

  ngOnDestroy() {
  }

}
