import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { Person } from '../person';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-people',
  templateUrl: './list-people.component.html',
  styleUrls: ['./list-people.component.css']
})
export class ListPeopleComponent implements OnInit {

  displayedColumns: string[] = ['id', 'fullName', 'address', 'age', 'interests', 'picturePath'];
  data: Person[] = [];
  isLoadingResults = true;

  constructor(private api: ApiService, private router: Router) { }

  ngOnInit() {
    this.api.searchPersons("")
      .subscribe(res => {
        this.data = res;
        console.log(this.data);
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }
  onSubmit(value) {
    this.isLoadingResults = true;
    this.api.searchPersons(value)
      .subscribe(res => {
        this.data = res;
        console.log(this.data);
        this.isLoadingResults = false;
      }, err => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }
}
