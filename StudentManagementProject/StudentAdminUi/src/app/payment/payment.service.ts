// payment.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Payment } from '../models/api-models/payment.model';
import { PaymentUpdateRequest } from '../models/api-models/updatepaymentrequest.model';



@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private baseApiurl = 'https://localhost:44316';

  constructor(private httpClient: HttpClient) { }

  getPayments(): Observable<Payment[]> {
    return this.httpClient.get<Payment[]>(`${this.baseApiurl}/Payment`);
  }

  getPayment(paymentId: string | null): Observable<Payment> {
    return this.httpClient.get<Payment>(`${this.baseApiurl}/payment/${paymentId}`);
  }

  updatePayment(paymentId:string,paymentrequest:Payment): Observable<Payment>{
    const updatepaymentrequest :PaymentUpdateRequest ={
       PaymentType:paymentrequest.paymentType,
       InstallmentCount:paymentrequest.installmentCount

    }
    return this.httpClient.put<Payment>(this.baseApiurl+'/payment/'+paymentId,updatepaymentrequest);
  }


 
 
  
}

