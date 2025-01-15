import {  HttpHeaders,HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { Estudents } from '../models/Estudents.model';
import { CourseByEstudent } from '../models/CourseByEstudent.model';
import { CourseByTeacher } from '../models/CourseByTeacher.model';


@Injectable({
  providedIn: 'root',
})

export class DataEstudentsService {

  private apiUrl = environment.apiUrl;
  public http = inject(HttpClient);
  private getHeaders(): HttpHeaders {
    // const token = sessionStorage.getItem('token');
    return new HttpHeaders({
      // 'Authorization': `Bearer ${token}`
      'Access-Control-Allow-Origin':"*"
    });
  }
  constructor() { }

  saveEstudents(estudents:any,complemento: string): Observable<Estudents> {
    const headers = this.getHeaders();
    return this.http.post<Estudents>(`${this.apiUrl}/${complemento}`, estudents,{ headers});
  }
  getCountCourseByEstudent(complemento: string,id: number): Observable<CourseByEstudent> {
    const headers = this.getHeaders();
    return this.http.get<CourseByEstudent>(`${this.apiUrl}/${complemento}/${id}`, { headers });
  }

  getAllCourse(complemento: string): Observable<CourseByEstudent> {
    const headers = this.getHeaders();
    return this.http.get<CourseByEstudent>(`${this.apiUrl}/${complemento}`, { headers });
  }
  getCourseByEstudent(complemento: string,id:number): Observable<CourseByEstudent> {
    const headers = this.getHeaders();
    return this.http.get<CourseByEstudent>(`${this.apiUrl}/${complemento}/${id}`, { headers });
  }
  getCourseByTeacher(complemento: string): Observable<any[]> {
    const headers = this.getHeaders();
    return this.http.get<any[]>(`${this.apiUrl}/${complemento}`, { headers });
  }

  saveCourse(Registro:any,complemento: string): Observable<any> {
    const headers = this.getHeaders();
    return this.http.post<any>(`${this.apiUrl}/${complemento}`,Registro );
  }
  getCourseById(complemento: string,id:number): Observable<CourseByEstudent> {
    const headers = this.getHeaders();
    return this.http.get<CourseByEstudent>(`${this.apiUrl}/${complemento}/${id}`, { headers });
  }
  deleteCourseById(complemento: string,idEstudent:number, idCourse:number): Observable<any[]> {
    const headers = this.getHeaders();
    return this.http.delete<any[]>(`${this.apiUrl}/${complemento}/${idEstudent}/${idCourse}`, { headers });
  }

}
