import { Injectable } from '@angular/core';
import { Observable, of, throwError } from 'rxjs';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap, map } from 'rxjs/operators';
import { Person } from './person';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const apiUrl = 'http://localhost:5000/api';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      //console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  getPersons(): Observable<Person[]> {
    const url = `${apiUrl}/People`;
    return this.http.get<Person[]>(url)
      .pipe(
        tap(_ => console.log('fetched Persons')),
        catchError(this.handleError('getPersons', []))
      );
  }

  searchPersons(term: string): Observable<Person[]> {
    const url = `${apiUrl}/People/search/${term}`;
    return this.http.get<Person[]>(url)
      .pipe(
        tap(_ => console.log('fetched Persons')),
        catchError(this.handleError('searchPersons', []))
      );
  }

  getPerson(id: number): Observable<Person> {
    const url = `${apiUrl}/People/${id}`;
    return this.http.get<Person>(url).pipe(
      tap(_ => console.log(`fetched Person id=${id}`)),
      catchError(this.handleError<Person>(`getPerson id=${id}`))
    );
  }

  addPerson(Person: any): Observable<Person> {
    const url = `${apiUrl}/People`;
    return this.http.post<Person>(url, Person, httpOptions).pipe(
      tap((PersonRes: Person) => console.log(`added Person w/ id=${PersonRes.id}`)),
      catchError(this.handleError<Person>('addPerson'))
    );
  }

  updatePerson(id: number, Person: any): Observable<any> {
    const url = `${apiUrl}/People/${id}`;
    return this.http.put(url, Person, httpOptions).pipe(
      tap(_ => console.log(`updated Person id=${id}`)),
      catchError(this.handleError<any>('updatePerson'))
    );
  }

  deletePerson(id: number): Observable<Person> {
    const url = `${apiUrl}/People/${id}`;
    return this.http.delete<Person>(url, httpOptions).pipe(
      tap(_ => console.log(`deleted Person id=${id}`)),
      catchError(this.handleError<Person>('deletePerson'))
    );
  }
}
