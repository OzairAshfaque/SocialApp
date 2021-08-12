import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { map } from 'rxjs/operators'
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Social App';
  users : any;

  constructor(private http : HttpClient, private accountService :AccountService ){}
  ngOnInit() {

  this.setCurrentUser();
  }
  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user')||'{}');
    console.log(user.username+"I am HERE");
    if(user.username!=null)
    {
      this.accountService.setCurrentUser(user);
    }
    
  }
  
    
  }
  

