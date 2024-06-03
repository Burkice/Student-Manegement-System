import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Student } from '../models/api-models/student.model';
import { updateStudentRequest } from '../models/api-models/updateStudentRequest.model';
import { addStudentRequest } from '../models/api-models/addStudentRequest.model';


@Injectable({
  providedIn: 'root'
})
export class StudentsService {
  private baseApiurl='https://localhost:44316';

  constructor(private httpClient:HttpClient) { }

  getStudents(): Observable<Student[]>{
    return this.httpClient.get<Student[]>(this.baseApiurl+'/Student');
  }

  getStudent(studentId:string | null): Observable<Student>{
    return this.httpClient.get<Student>(this.baseApiurl+'/student/'+studentId);
  }

  UpdateStudent(studentId:string,studentRequest:Student): Observable<Student>{
    const updateStudentRequest :updateStudentRequest ={
         Firstname:studentRequest.firstname,
         Lastname:studentRequest.lastname,
         DateofBirth:studentRequest.dateofBirth,
         Email:studentRequest.email,
         Phone:studentRequest.phone,
         Gender:studentRequest.gender,
         PhysicalAddress:studentRequest.address.physicalAddress,
         PostalAddress:studentRequest.address.postalAddress


    }
    return this.httpClient.put<Student>(this.baseApiurl+'/student/'+studentId,updateStudentRequest);
  }

  deleteStudent(studentId:string): Observable<Student>{
   
    return this.httpClient.delete<Student>(this.baseApiurl+'/student/'+studentId);
  }


  addStudent(studentRequest:Student): Observable<Student>{
    const addStudentRequest :addStudentRequest ={
         Firstname:studentRequest.firstname,
         Lastname:studentRequest.lastname,
         DateofBirth:studentRequest.dateofBirth,
         Email:studentRequest.email,
         Phone:studentRequest.phone,
         Gender:studentRequest.gender,
         PhysicalAddress:studentRequest.address.physicalAddress,
         PostalAddress:studentRequest.address.postalAddress


    }
    return this.httpClient.post<Student>(this.baseApiurl+'/student/add',addStudentRequest);
  }


}
