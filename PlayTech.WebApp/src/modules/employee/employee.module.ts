import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeParentComponent } from './components/employee-parent/employee-parent.component';
import { EmployeeListComponent } from './components/employee-list/employee-list.component';
import { EmployeeEditComponent } from './components/employee-edit/employee-edit.component';
import {SharedModule} from '../../shared/shared.module';

const components = [
  EmployeeParentComponent,
  EmployeeListComponent,
  EmployeeEditComponent
];

@NgModule({
  declarations: [
    ...components
  ],
  imports: [
    CommonModule,
    SharedModule
  ],
  exports: [
    ...components
  ]
})
export class EmployeeModule { }
