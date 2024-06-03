import { Component, OnInit } from '@angular/core';
import { InstallmentService } from './installment.service';
import { Installment } from '../models/ui-models/installment.model';
import { MatTableDataSource } from '@angular/material/table';
import { error } from 'console';

@Component({
  selector: 'app-installment',
  templateUrl: './installment.component.html',
  styleUrl: './installment.component.css'
})
export class InstallmentComponent implements OnInit {
installment:Installment[]=[];
displayedColumns: string[] = ['id', 'periodId', 'installmentC', 'installmentt','paymentStatus','student','edit'];
dataSource:MatTableDataSource<Installment>= new MatTableDataSource<Installment>();
filterString = '';
  constructor(private installmentService:InstallmentService) {}
   
    
 
  ngOnInit(): void {
    debugger;
    this.installmentService.getinstallment().subscribe(
      (success)=>{
         this.installment=success;
         this.dataSource= new MatTableDataSource<Installment>(this.installment);

         
      },
      (error)=>{

      }
    )
    
  }

  filterStudents(){
    this.dataSource.filter=this.filterString.trim().toLocaleLowerCase();
  }
}
