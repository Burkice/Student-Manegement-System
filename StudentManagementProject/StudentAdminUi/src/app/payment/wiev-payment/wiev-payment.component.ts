// wiev-payment.component.ts
import { Component, OnInit } from '@angular/core';
import { PaymentService } from '../payment.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Payment } from '../../models/api-models/payment.model';
import { error } from 'console';

@Component({
  selector: 'app-wiev-payment',
  templateUrl: './wiev-payment.component.html',
  styleUrls: ['./wiev-payment.component.css']
})
export class WievPaymentComponent implements OnInit {
  paymentId: string | null | undefined;
  payment: Payment = {
    id: '',
    installmentCount: 0,
    paymentType: '',
    totalCoursefee:0,
  };

  constructor(
    private readonly paymentService: PaymentService,
    private readonly route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id !== null) {
        this.paymentId = id;
        this.paymentService.getPayment(this.paymentId).subscribe(
          success => {
            this.payment = success;
          },
          error => {}
        );
      } else {
        // Null olduğunda yapılacak işlem
      }
    });
  }

  onUpdate(){
    debugger;
    this.paymentService.updatePayment(this.payment.id,this.payment)
    .subscribe(
      (success)=>{
            debugger;
            this.router.navigateByUrl('payment');
      },
      (error)=>{

      }
    )
  }
  
}
