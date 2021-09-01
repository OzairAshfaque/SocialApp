import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users :any;
  testVar? : string ;


  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  //  this.getUsers();
    this.testVar = "I am the value coming here."
  }

  registerToggle(){
    this.registerMode = !this.registerMode;
  }
  getUsers()
  {
    this.http.get("https://localhost:5001/api/users").subscribe(response =>
    {
      this.users = response;


      console.log(this.users);
    },error=>
    {
      console.log(error);
    });

  }
  cancelRegisterMode(event : boolean){
    this.registerMode = event;
  }

  
}
