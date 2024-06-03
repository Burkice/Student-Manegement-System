import { Component, OnInit } from '@angular/core';
import { PaymenthistoryService } from './paymenthistory.service';
import { error } from 'console';
import { Paymenthistory } from '../models/ui-models/paymenthistory.model';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-paymenthistory',
  templateUrl: './paymenthistory.component.html',
  styleUrl: './paymenthistory.component.css'
})
export class PaymenthistoryComponent implements OnInit {
paymentHistorys:Paymenthistory[]=[]
displayedColumns: string[] = ['id','periodId','installmentId', 'paymentAmount', 'recordStatus', 'paymentType'];
dataSource:MatTableDataSource<Paymenthistory> = new MatTableDataSource<Paymenthistory>();
  
  constructor(private paymentHistoryService:PaymenthistoryService) {
    
    
  }

  ngOnInit(): void {
    debugger;
    this.paymentHistoryService.getPaymentHistory().subscribe(
      (success)=>{
         this.paymentHistorys=success;
         this.dataSource = new MatTableDataSource<Paymenthistory>(this.paymentHistorys);
      },
      (error)=>{

      }
    )
  }
}
