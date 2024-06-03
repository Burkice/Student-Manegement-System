import { Component, OnInit, ViewChild } from '@angular/core';
import { StudentCourseDTO } from '../models/ui-models/studentcoursedto.model';
import { MatTableDataSource } from '@angular/material/table';
import { StudentcourseService } from './studentcourse.service';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';


@Component({
  selector: 'app-studentcourse',
  templateUrl: './studentcourse.component.html',
  styleUrl: './studentcourse.component.css'
})
export class StudentcourseComponent implements OnInit {
  studentCourse:StudentCourseDTO[]=[];
  displayedColumns: string[] = ['studentCourseId','studentId', 'studentName','courseId' ,'courseName'];
 dataSource:MatTableDataSource<StudentCourseDTO>= new MatTableDataSource<StudentCourseDTO>();
 @ViewChild(MatPaginator) paginator!: MatPaginator;
 @ViewChild(MatSort) sort!: MatSort;
 filterString = '';
  constructor(private readonly studentCourseService:StudentcourseService,
  ) {  }




  ngOnInit(): void {
    debugger;
    this.studentCourseService.getStudentsCourse().subscribe(
      (success)=>{
        this.studentCourse=success;
        this.dataSource=new MatTableDataSource<StudentCourseDTO>(this.studentCourse);
        this.dataSource.paginator=this.paginator;
        this.dataSource.sort = this.sort;
      }
    )
    
  }

  filterStudentscOURSE(){
    this.dataSource.filter=this.filterString.trim().toLocaleLowerCase();
  } 
}
