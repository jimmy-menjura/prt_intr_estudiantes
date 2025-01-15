import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Logon } from '../models/Logon.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = environment.apiUrlAuth;
  public http = inject(HttpClient);
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      // 'Authorization': `Bearer ${token}`
      'Access-Control-Allow-Origin':"*"
    });
  }
  constructor() { }

  LogonEstudents(logon:any,complemento: string): Observable<Logon> {
    const headers = this.getHeaders();
    return this.http.post<Logon>(`${this.apiUrl}/${complemento}`, logon,{ headers});
  }
  
}