import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { PhoneBook } from '../interfaces/phone-book';
import { PhoneBookEntry } from '../interfaces/phone-book-entry';
 
@Injectable({
  providedIn: 'root'
})
export class PhoneBooksService {
  url = 'http://localhost:5000/api/contacts';
  constructor(public http: HttpClient) {}
 
  getPhoneBooks(): Observable<PhoneBook[]> {
    return this.http.get<PhoneBook[]>(`${this.url}/phonebooks`);
  }

  searchPhoneBooks(term: string): Observable<PhoneBook[]> {
    if(term)
    {
      return this.http.get<PhoneBook[]>(`${this.url}/phonebooks/search/${term}`);
    }

    return this.http.get<PhoneBook[]>(`${this.url}/phonebooks`);
  }

  getPhoneBook(id: number) : Observable<PhoneBook> {
    return this.http.get<PhoneBook>(`${this.url}/phonebooks/${id}`);
  }

  createEntry(entry: PhoneBookEntry): Observable<any> {
    return this.http.post<PhoneBook>(`${this.url}/createentry`, entry);
  }
}