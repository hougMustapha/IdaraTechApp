import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class StartService {

  constructor(private http: HttpClient) { }

  getStarters(){
    return this.http.get(`${environment.appUrl}/api/start/get-starters`);
  }
}
