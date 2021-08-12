import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model : any = {}
 // loggedIn : boolean = false;
  //currentUser$ ? : Observable<User | null>;
 // subscription : any;

  constructor(public accountService : AccountService, private router : Router, 
    private toastr : ToastrService ) { }

  ngOnInit(): void {
    //this.getCurrentUser();
    //this.currentUser$ = this.accountService.currentUser$;
  }
  
  login(){
    //console.log(this.model);
     this.accountService.login(this.model).subscribe(response=>
      {
        console.log(response);
        this.router.navigateByUrl('/members');
        
       // this.loggedIn = true;
      },error =>{
        console.log(error);
        this.toastr.error(error.error);
      });    
      
  }
  logout()
  {
    //this.loggedIn = false;
    this.accountService.logout();
    this.router.navigateByUrl('/');
    }
//deprecated method as unsubscribing
  getCurrentUser(){
    this.accountService.currentUser$.subscribe(user =>{
     // this.loggedIn = !! user;
    }, error =>{
      console.log(error);
    });
  }
  ngOnDestroy() {
   // this.subscription.unsubscribe()
}
  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user')||'{}');
    console.log(user.username+" I am HERE");
    if(user.username!=null){
      this.accountService.setCurrentUser(user);
    }
    
  }

}
