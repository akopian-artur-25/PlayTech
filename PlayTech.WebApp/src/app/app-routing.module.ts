import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {EmployeeParentComponent} from '../modules/employee/components/employee-parent/employee-parent.component';
import {EmployeeListComponent} from '../modules/employee/components/employee-list/employee-list.component';


const routes: Routes = [
  {
    path: '',
    component: EmployeeParentComponent,
    children: [
      {
        path: '',
        component: EmployeeListComponent,
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
