import { PhoneBookEntry } from "./phone-book-entry";

export interface PhoneBook {
    Id: number;
    Name: string;
    Entries: Array<PhoneBookEntry>
}