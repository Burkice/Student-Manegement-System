import { Component, OnInit } from '@angular/core';
import { StudentsService } from '../students.service';
import { ActivatedRoute, Router } from '@angular/router';
import { error } from 'console';
import { Studentss } from '../../models/ui-models/student.model';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-wiev-student',
  templateUrl: './wiev-student.component.html',
  styleUrl: './wiev-student.component.css'
})
export class WievStudentComponent  implements OnInit{
   studentId:string | null | undefined;
   student:Studentss = {
    id:'',
    firstname:'',
    lastname:'',
    dateofBirth:'',
    email:'',
    phone:0,
    gender:'',
    address:{
      id:'',
      physicalAddress:'',
      postalAddress:''
    }
   };

   isNewStudent=false;
   header="";



  constructor(private readonly studentService:StudentsService,
    private readonly route:ActivatedRoute,
     private router:Router,
     private snackbar:MatSnackBar
     
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(
      (params)=>{
        debugger;
        this.studentId = params.get('id');
        //studenid add ise eklemeye göre
          if(this.studentId === 'add')
            {
                this.isNewStudent =true;
                this.header ="Öğrenci Ekle";
            }
            else{
              this.isNewStudent = false;
              this.header ="Öğrenci Düzenle";
              this.studentService.getStudent(this.studentId).subscribe(
                (success)=>{
                this.student=success;
                },
                (error)=>{
      
                }
              )
            }

      


       
      }
    )
  }

  onUpdate(){
    debugger;
    this.studentService.UpdateStudent(this.student.id,this.student)
    .subscribe(
      (success)=>{
            debugger;
            this.router.navigateByUrl('students');
      },
      (error)=>{

      }
    )
  }
  onDelete(){
    this.studentService.deleteStudent(this.student.id).subscribe(
      (success)=>
        {
            this.snackbar.open('Öğrenci başarılı bir şekilde silini',undefined,{
              duration:2000
            })

            setTimeout(()=>{
              this.router.navigateByUrl('students');
            },2000)
            
        },
        (error)=>{

        }
    )
  }
  onAdd(){
    
    this.studentService.addStudent(this.student)
    .subscribe(
      (success)=>{
            debugger;
            this.snackbar.open('Öğrenci Başarılı bir şekilde eklendi!',undefined,{
              duration:2000
            })
            setTimeout(()=>{
              this.router.navigateByUrl(`students/${success.id}`);
            },2000)
           
      },
      (error)=>{

      }
    )
  }
}
