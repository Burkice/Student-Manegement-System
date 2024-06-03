import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Teacher } from '../models/api-models/Teacher.Model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TeacherService {
  private baseApiurl='https://localhost:44316';

  constructor(private httpClient:HttpClient) { }


  getTeacher(): Observable<Teacher[]>{
    return this.httpClient.get<Teacher[]>(this.baseApiurl+'/Teacher');
  }
}
