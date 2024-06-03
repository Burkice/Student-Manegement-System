import { Component, OnInit,ViewChild } from '@angular/core';
import { StudentsService } from './students.service';
import { error } from 'console';
import { Studentss } from '../models/ui-models/student.model';
import { MatTable, MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrl: './students.component.css'
})
export class StudentsComponent  implements OnInit{
 students:Studentss[]=[];
 displayedColumns: string[] = ['id', 'firstname', 'lastname', 'dateofBirth','email','phone','gender','edit'];
 dataSource:MatTableDataSource<Studentss>= new MatTableDataSource<Studentss>();
 @ViewChild(MatPaginator) paginator!: MatPaginator;
 @ViewChild(MatSort) sort!: MatSort;
 filterString = '';
  constructor(private studentService:StudentsService){}

  ngOnInit(): void {
    debugger;
    this.studentService.getStudents().subscribe(
      (success) =>{
            this.students=success;
            this.dataSource=new MatTableDataSource<Studentss>(this.students);
            this.dataSource.paginator=this.paginator;
            this.dataSource.sort = this.sort;
      },
      (error)=>{

      }

    )
  }
  filterStudents(){
    this.dataSource.filter=this.filterString.trim().toLocaleLowerCase();
  }
}
