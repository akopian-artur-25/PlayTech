import {EmployeeListItem} from './employee-list-item.model';
import {EmployeeEdit} from './employee-edit.model';

export class EmployeeHelper {
  static getFirstName(model: EmployeeListItem | EmployeeEdit) {
    const nameChunks = model?.name?.split(' ');
    return nameChunks?.length > 0 ? nameChunks[0] : '';
  }
  static getLastName(model: EmployeeListItem | EmployeeEdit) {
    const nameChunks = model?.name?.split(' ');
    return nameChunks?.length > 1 ? nameChunks[1] : '';
  }
}
