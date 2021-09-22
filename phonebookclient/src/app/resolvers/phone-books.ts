import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Resolve } from '@angular/router';
import { Observable, of } from 'rxjs';
import { PhoneBook } from '../interfaces/phone-book';
import { PhoneBooksService } from '../services/phone-book.service';
 
@Injectable({
  providedIn: 'root'
})
export class PhoneBooksResolverService implements Resolve<PhoneBook[]> {
  constructor(private phonebookservice: PhoneBooksService) {}
  resolve(route: ActivatedRouteSnapshot): Observable<any> {

    return this.phonebookservice.getPhoneBooks().pipe();

  }
}