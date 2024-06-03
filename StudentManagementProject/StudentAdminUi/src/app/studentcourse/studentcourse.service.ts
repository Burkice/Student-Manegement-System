import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { StudentCourseDTO } from '../models/api-models/studentcoursedto.model';
import { AddStudentCourseRequest } from '../models/api-models/addstudentcourserequest.model';

@Injectable({
  providedIn: 'root'
})
export class StudentcourseService {
private baseApiUrl='https://localhost:44316';
  constructor(private httpClient:HttpClient) { }

  getStudentsCourse(): Observable<StudentCourseDTO[]>{
    return this.httpClient.get<StudentCourseDTO[]>(this.baseApiUrl+'/StudentCourse');
  }


  addStudentCourses(request: AddStudentCourseRequest): Observable<any> {
    return this.httpClient.post(`${this.baseApiUrl}/studentcourse/addstudentcourses`, request);
  }
 
 
  
 
}
