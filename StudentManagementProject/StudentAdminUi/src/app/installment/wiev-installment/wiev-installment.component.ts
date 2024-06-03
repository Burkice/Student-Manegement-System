import { Component } from '@angular/core';
import { InstallmentService } from '../installment.service';
import { error } from 'console';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-wiev-installment',
  templateUrl: './wiev-installment.component.html',
  styleUrl: './wiev-installment.component.css'
})
export class WievInstallmentComponent {
  installmentId:number=0;
  amountPaid:number=0;
  response:any;

  constructor(private installmentService:InstallmentService,
    private router:Router
  ){}


  payInstallment() {
    this.installmentService.payInstallment(this.installmentId, this.amountPaid)
      .subscribe(
        data => {
          this.response = data;
          console.log('Taksit Ödemesi Başarılı', data);
        },
        error => {
          console.error('Ödeme İşlemi Başarısız oldu', error);
        }
      );
  }
  goBack() {
    this.router.navigate(['/installment']);
  }


}
