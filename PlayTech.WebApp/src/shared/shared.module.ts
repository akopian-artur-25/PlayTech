import {CommonModule} from '@angular/common';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MaterialModule} from './material.module';
import {RouterModule} from '@angular/router';
import {HttpClientModule} from '@angular/common/http';
import {TableButtonsComponent} from './components/table-buttons/table-buttons.component';

const components = [
  TableButtonsComponent
];

const modules = [
  MaterialModule,
  ReactiveFormsModule,
  HttpClientModule,
  FormsModule,
  RouterModule
];

@NgModule({
  declarations: [
    ...components
  ],
  imports: [
    CommonModule,
    ...modules
  ],
  exports: [
    ...modules,
    ...components
  ],
})
export class SharedModule { }
