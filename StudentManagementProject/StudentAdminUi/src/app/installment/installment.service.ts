import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Installment } from '../models/api-models/installment.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class InstallmentService {
  private baseApiUrl='https://localhost:44316';


  constructor(private httpClient:HttpClient) { }

  getinstallment(): Observable<Installment[]>{
    return this.httpClient.get<Installment[]>(this.baseApiUrl+'/Installment');
  }
  payInstallment(installmentId: number, amountPaid: number): Observable<any> {
    const url = `${this.baseApiUrl}/Installment/pay`;
    const body = { installmentId: installmentId, amountPaid: amountPaid };
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };

    return this.httpClient.post(url, body, httpOptions);
  }


 

  
}
