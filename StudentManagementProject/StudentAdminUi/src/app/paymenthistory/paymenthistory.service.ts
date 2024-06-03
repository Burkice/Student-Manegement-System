import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Paymenthistory } from '../models/api-models/paymenthistory.model';

@Injectable({
  providedIn: 'root'
})
export class PaymenthistoryService {

  private baseApiUrl='https://localhost:44316';

  constructor(private httpClient:HttpClient) { }

  getPaymentHistory():Observable<Paymenthistory[]>{
    return this.httpClient.get<Paymenthistory[]>(this.baseApiUrl+'/PaymentHistory')
  }
}
