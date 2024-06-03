import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  baseApiUrl='https://localhost:44316';

  constructor(private httpClient:HttpClient) { }


  login(userName:string,password:string){
    return this.httpClient.post<{token:string}>(this.baseApiUrl + '/api/Auth/login',{
      "userName": userName,
      "password": password,
    });
  }
}
