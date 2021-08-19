import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.css']
})
export class NotFoundComponent implements OnInit {
  isUser : boolean = false;
  constructor(public accountService : AccountService, private http:HttpClient) { }

  ngOnInit(): void {
    this.isLogin();
    this.getUserFake();
    
  }
  isLogin(){
   return this.accountService.crtUser$.subscribe(user =>{
    if(user)
    {
      this.isUser = true;
      console.log(this.isUser);
    }

   }
   );
    
  }
  getUserFake(){
    return this.http.get("https://localhost:5001/api/users").subscribe(x=>
    {
      console.log(x);
    }
      
    );
  }


}
