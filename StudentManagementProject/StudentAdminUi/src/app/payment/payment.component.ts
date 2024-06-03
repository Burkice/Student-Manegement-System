import { Component, OnInit } from '@angular/core';
import { PaymentService } from './payment.service';
import { MatTableDataSource } from '@angular/material/table';
import { Payments } from '../models/ui-models/payment.model';


@Component({
  selector: 'app-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.css']
})
export class PaymentComponent implements OnInit {
  payment: Payments[] = [];
  displayedColumns: string[] = ['id', 'studentId', 'totalCourseFee', 'paymenttype', 'installmentCount', 'courseId', 'edit'];
  dataSource: MatTableDataSource<Payments> = new MatTableDataSource<Payments>();
 
  constructor(private paymentService: PaymentService) { }

  ngOnInit(): void {
    this.paymentService.getPayments().subscribe(
      (success) => {
        this.payment = success;
        this.dataSource = new MatTableDataSource<Payments>(this.payment);
      },
      (error) => {
       
      }
    );
  }
}
