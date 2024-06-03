import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WievStudentComponent } from './wiev-student.component';

describe('WievStudentComponent', () => {
  let component: WievStudentComponent;
  let fixture: ComponentFixture<WievStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WievStudentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WievStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
