import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.scss']
})
export class PhonebookComponent implements OnInit {
  @Input() name: string;
  @Input() contactnumber: string;
  
  constructor() { }

  ngOnInit(): void {
  }

}
