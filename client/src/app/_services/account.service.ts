import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource.asObservable();
  crtUser$ = this.currentUserSource.asObservable();
  obj : any;
  

  constructor(private http : HttpClient) { }

  register(model : any){
   // console.log(model);
   console.log(this.baseUrl+'account/register');
    return this.http.post<User>(this.baseUrl+'account/register',model).pipe(
      map((user:User)=>{
        if(user){
          console.log(user);
          localStorage.setItem('user',JSON.stringify(user));
          this.currentUserSource.next(user);
        }
        return user;
      }
      ));
  }
  login(model : any){
    //return this.http.post(this.baseUrl+'account/login',model);
    return  this.http.post<User>(this.baseUrl+'account/login',model).pipe(
      map((response:User)=>
        {
          console.log(response.username);
          const user = response;
          if(user)
          {
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUserSource.next(user);

          }
          return user;
        })
    );
  }

  setCurrentUser(user:User){
    this.currentUserSource.next(user);
    console.log(user);
    console.log(user.username);
  }

  logout(){
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    console.log(this.currentUserSource.subscribe(x=>x?.username));
  }
}
