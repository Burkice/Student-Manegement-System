import { Component, OnInit } from '@angular/core';
import { CourseService } from './course.service';
import { error } from 'console';
import { Course } from '../models/ui-models/course.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrl: './course.component.css'
})
export class CourseComponent implements OnInit {
courses:Course[]=[];
displayedColumns: string[] = ['id', 'name', 'credit'];
dataSource:MatTableDataSource<Course> =new MatTableDataSource<Course>();

  /**
   *
   */
  constructor(private courseService:CourseService) { }

  ngOnInit(): void {
    this.courseService.getCourse().subscribe(
      (success)=>{
       this.courses = success;
       this.dataSource=new MatTableDataSource<Course>(this.courses);
      },
      (error)=>{

      }
    )
  }
}
