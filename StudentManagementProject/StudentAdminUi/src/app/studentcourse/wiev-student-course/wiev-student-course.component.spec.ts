import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WievStudentCourseComponent } from './wiev-student-course.component';

describe('WievStudentCourseComponent', () => {
  let component: WievStudentCourseComponent;
  let fixture: ComponentFixture<WievStudentCourseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WievStudentCourseComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WievStudentCourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
