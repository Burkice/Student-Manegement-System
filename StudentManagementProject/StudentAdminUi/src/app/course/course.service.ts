import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Course } from '../models/api-models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private baseApiUrl='https://localhost:44316';

  constructor(private htppClient:HttpClient) { }

  getCourse():Observable<Course[]>{
 return this.htppClient.get<Course[]>(this.baseApiUrl+'/Course');

  }
}
