import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PhoneBooksService } from '../services/phone-book.service';
import { PhoneBookEntry } from '../interfaces/phone-book-entry';

@Component({
  selector: 'app-create-entry',
  templateUrl: './create-entry.component.html',
  styleUrls: ['./create-entry.component.scss']
})
export class CreateEntryComponent implements OnInit {

  constructor(private route: ActivatedRoute, private phoneBookService: PhoneBooksService, private router: Router) { }

  private phoneBookId: number;
  public phoneBookName: string;
  public count: number;
  
  ngOnInit(): void {

    this.route.params.subscribe((params: Params) => {
      this.phoneBookId = params['phoneBookId'];
      this.phoneBookService.getPhoneBook(this.phoneBookId).subscribe((r) => {
        this.phoneBookName = r[0].name;
        this.count = r[0].entries.length
      })
    });

    
  }

  public onSubmit(form: NgForm): void {
    const name = form.value.name;
    const contactNumber = form.value.contact;

    const entry: PhoneBookEntry = {
      PhoneBookId: this.phoneBookId,
      Name: name,
      ContactNumber: contactNumber
    }

    if(!entry.Name)
    {
       alert("Name is required");
       return;
    }

    if(!entry.ContactNumber)
    {
       alert("Contanct number is required");
       return;
    }

    if(entry.ContactNumber.length != 10)
    {
       alert("Contanct number must have 10 digits");
       return;
    }

    this.phoneBookService.createEntry(entry).subscribe((r) => {
      this.router.navigate(['/']);
    }, (e) => {
      this.router.navigate(['/']);
    })
  }

  public cancel(){;
    this.router.navigate(['/']);
  }
}
