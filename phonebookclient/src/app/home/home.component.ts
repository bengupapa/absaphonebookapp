import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators'
import { PhoneBook } from '../interfaces/phone-book';
import { PhoneBooksService } from '../services/phone-book.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute, private phoneBookService: PhoneBooksService) { }

  public phonebooks$: Observable<PhoneBook[]>;

  ngOnInit(): void {
    this.phonebooks$  = this.activatedRoute.data.pipe(map(x => <PhoneBook[]>x.products));
  }

  search(term): void {
    this.phonebooks$  =  this.phoneBookService.searchPhoneBooks(term).pipe();
  }

}
