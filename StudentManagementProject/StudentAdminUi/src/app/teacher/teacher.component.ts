import { Component, OnInit } from '@angular/core';
import { TeacherService } from './teacher.service';
import { Teacher } from '../models/ui-models/teacher.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-teacher',
  templateUrl: './teacher.component.html',
  styleUrl: './teacher.component.css'
})
export class TeacherComponent implements OnInit {
  teacher:Teacher[]=[];
  displayedColumns: string[] = ['id', 'firstname', 'lastname','email','phone'];
  dataSource:MatTableDataSource<Teacher>= new MatTableDataSource<Teacher>();

  
  constructor(private teacherService:TeacherService) {
  
    
  }


  ngOnInit(): void {
    debugger;
    this.teacherService.getTeacher().subscribe(
      (success) =>{
            this.teacher=success;
            this.dataSource=new MatTableDataSource<Teacher>(this.teacher);
            
      },
      (error)=>{

      }

    )
  }
}
