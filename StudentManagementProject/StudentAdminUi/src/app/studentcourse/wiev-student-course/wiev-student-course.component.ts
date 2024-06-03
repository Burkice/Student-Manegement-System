import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { StudentcourseService } from '../studentcourse.service';
import { AddStudentCourseRequest } from '../../models/api-models/addstudentcourserequest.model';

@Component({
  selector: 'app-wiev-student-course',
  templateUrl: './wiev-student-course.component.html',
  styleUrls: ['./wiev-student-course.component.css']
})
export class WievStudentCourseComponent {
  addCourseForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private studentCourseService: StudentcourseService,
    private snackBar: MatSnackBar,
    private router: Router
  ) {
    this.addCourseForm = this.fb.group({
      studentName: ['', Validators.required],
      courseNames: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.addCourseForm.valid) {
      const formValue = this.addCourseForm.value;
      const request: AddStudentCourseRequest = {
        studentName: formValue.studentName,
        courseNames: formValue.courseNames.split(',').map((name: string) => name.trim())
      };

      this.studentCourseService.addStudentCourses(request).subscribe(
        response => {
          console.log('Courses added successfully', response);
          this.snackBar.open('Courses added successfully', 'Close', {
            duration: 3000,
          });
          this.router.navigate(['/studentcourse']);
        },
        error => {
          console.error('Error adding courses', error);
          this.snackBar.open('Error adding courses', 'Close', {
            duration: 3000,
          });
        }
      );
    }
  }
}
