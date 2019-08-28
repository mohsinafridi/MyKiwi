import { User } from './../_models/user';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';


// Now Token will attached in header  from jwt helper from app.module
@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    // debugger;
    return this.http.get<User[]>('http://localhost:5000/api/users');
  }

  getUser(id): Observable<User> {
    return this.http.get<User>('http://localhost:5000/api/users/' + id);
  }

}
