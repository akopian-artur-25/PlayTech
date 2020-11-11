import {BehaviorSubject} from 'rxjs';
import {FormGroup} from '@angular/forms';
import {cloneDeep, isEqual} from 'lodash';
import { OnDestroy, OnInit, Directive } from '@angular/core';
import {deleteNullProperties} from '../utils/null';
import {FormHelper} from '../utils/forms/form-helper';
import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';
import {ModeType} from '../enums/mode-type.enum';

@Directive()
@UntilDestroy()
export abstract class BaseEditComponent<TEdit> implements OnInit, OnDestroy {
  public readonly modeTypes = ModeType;

  public readonly isLoading$ = new BehaviorSubject<boolean>(false);

  public form: FormGroup;

  protected initialModel?: TEdit;
  model?: TEdit;

  public isFormOnceSubmitted = false;

  public mode: ModeType;
  public id?: number;

  get isInvalidFormView() {
    return this.isFormOnceSubmitted && (!this.form.valid || !this.validateForm());
  }

  get isModelChanged() {
    return !isEqual(this.model, this.initialModel);
  }


  protected constructor() {
  }

  ngOnInit() {
    this.id = this.initIdentifier();
    this.mode = this.initMode();
    this.form = this.initForm();
    this.refreshModel();
  }

  protected abstract initIdentifier();
  protected abstract initMode();
  protected abstract initForm();
  protected abstract refreshObservable();
  protected abstract refreshForm();
  protected abstract mapModel();

  protected refreshModel() {
    if (this.mode === ModeType.Edit) {
      this.isLoading$.next(true);
      this.refreshObservable().pipe(untilDestroyed(this))
        .subscribe((model: TEdit) => {
          this.model = model ? model : {} as TEdit;
          this.refreshInitModel();
          this.refreshForm();
          this.modelLoaded();
          this.isLoading$.next(false);
        }, (error) => {
          this.model = {} as TEdit;
          this.refreshInitModel();
          this.modelLoaded();
          this.isLoading$.next(false);
        });
    } else if (this.mode === ModeType.Create) {
      this.model = {} as TEdit;
      this.refreshInitModel();
      this.modelLoaded();
    }
  }

  protected modelLoaded() {

  }

  protected refreshInitModel() {
    deleteNullProperties(this.model);
    this.initialModel = cloneDeep(this.model);
  }

  protected prepareModel() {
    this.mapModel();
    deleteNullProperties(this.model);
  }

  protected validateForm() {
    return true;
  }

  submit() {
    this.isFormOnceSubmitted = true;
    FormHelper.validateAllFields(this.form);
    if (this.form.valid && this.validateForm()) {
      this.save();
    }
  }

  private save() {
    this.prepareModel();
    if (this.isModelChanged) {
      this.saveModel();
    }
    else {
      this.saveUnchangedModel();
    }
  }

  protected saveModel() {
  }

  protected saveUnchangedModel() {

  }

  ngOnDestroy() {
  }

}
