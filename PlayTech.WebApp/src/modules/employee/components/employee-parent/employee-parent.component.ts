import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-employee-parent',
  templateUrl: './employee-parent.component.html',
  styleUrls: ['./employee-parent.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeeParentComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
