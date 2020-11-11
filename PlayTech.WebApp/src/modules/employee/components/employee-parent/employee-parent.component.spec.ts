import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeParentComponent } from './employee-parent.component';

describe('EmployeeParentComponent', () => {
  let component: EmployeeParentComponent;
  let fixture: ComponentFixture<EmployeeParentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmployeeParentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeParentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
