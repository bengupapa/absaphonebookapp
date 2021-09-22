import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PhoneBook } from '../interfaces/phone-book';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private activatedRoute: ActivatedRoute) { }

  public phonebooks: Array<PhoneBook>;

  ngOnInit(): void {

    this.activatedRoute.data.subscribe((response: any) => {
  
      this.phonebooks = response.products;

    });

  }

}
