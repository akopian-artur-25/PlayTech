import {DepartmentInfo} from '../../department/models/department-info.model';
import {EmployeeInfo} from './employee-info.model';

export class EmployeeListItem {
  id: number;
  name: string;
  salary?: number;
  department?: DepartmentInfo;
  manager?: EmployeeInfo;
}
