import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListPeopleComponent } from './list-people/list-people.component';
import { AddPersonComponent } from './add-person/add-person.component';
import { PersonDetailComponent } from './person-detail/person-detail.component';
const routes: Routes = [{
  path: 'list-people',
  component: ListPeopleComponent,
  data: { title: 'List of People' }
},
  {
    path: 'person-detail/:id',
    component: PersonDetailComponent,
    data: { title: 'Person Details' }
  },
  {
    path: 'add-person',
    component: AddPersonComponent,
    data: { title: 'Add Person' }
  }
  //,
  //{
  //  path: '',
  //  redirectTo: '/people',
  //  pathMatch: 'full'
  //}
];
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
