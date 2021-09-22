import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { PhoneBook } from '../interfaces/phone-book';
 
@Injectable({
  providedIn: 'root'
})
export class PhoneBooksService {
  url = 'http://localhost:5000/api/contacts/phonebooks';
  constructor(public http: HttpClient) {}
 
  getPhoneBooks(): Observable<PhoneBook[]> {
    return this.http.get<PhoneBook[]>(this.url);
  }
}