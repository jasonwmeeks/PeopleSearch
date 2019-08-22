import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ApiService } from '../api.service';
import { Person } from '../person';

@Component({
  selector: 'app-person-detail',
  templateUrl: './person-detail.component.html',
  styleUrls: ['./person-detail.component.css']
})
export class PersonDetailComponent implements OnInit {

  person: Person = {
    id: null,
    fullName: '',
    age: 0,
    address: '',
    interests: '',
    picturePath: ''
  };
  isLoadingResults = true;

  constructor(private route: ActivatedRoute, private api: ApiService, private router: Router) { }

  ngOnInit() {
    this.getPersonDetails(this.route.snapshot.params['id']);
  }

  getPersonDetails(id) {
    this.api.getPerson(id)
      .subscribe(data => {
        this.person = data;
        console.log(this.person);
        this.isLoadingResults = false;
      });
  }
  deletePerson(id: number) {
    this.isLoadingResults = true;
    this.api.deletePerson(id)
      .subscribe(res => {
        this.isLoadingResults = false;
        this.router.navigate(['/person']);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      }
      );
  }
}
