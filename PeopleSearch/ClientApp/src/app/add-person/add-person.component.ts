import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ApiService } from '../api.service';
import { FormControl, FormGroupDirective, FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';

@Component({
  selector: 'app-add-person',
  templateUrl: './add-person.component.html',
  styleUrls: ['./add-person.component.css']
})
export class AddPersonComponent implements OnInit {

  personForm: FormGroup;
  fullName = '';
  age = '';
  address = '';
  interests = '';
  picturePath = '';
  isLoadingResults = false;

  matcher = new MyErrorStateMatcher();

  constructor(private router: Router, private api: ApiService, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.personForm = this.formBuilder.group({
      'fullName': [null, [Validators.required, Validators.maxLength(100)]],
      'age': [null, [Validators.required, Validators.pattern('^-?[0-9]\\d*(\\.\\d{1,2})?$')]],
      'address': [null, [Validators.required, Validators.maxLength(255)]],
      'interests': [null, Validators.required],
      'picturePath': [null, [Validators.required, Validators.maxLength(100)]],
    });
  }
  onFormSubmit(form: NgForm) {
    this.isLoadingResults = true;
    this.api.addPerson(form)
      .subscribe((res: { [x: string]: any; }) => {
        const person = res['personResponse'];
        const id = person['id'];
        this.isLoadingResults = false;
        this.router.navigate(['/person-detail', id]);
      }, (err) => {
        console.log(err);
        this.isLoadingResults = false;
      });
  }
}
/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}
