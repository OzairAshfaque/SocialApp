import { Component, Input, OnInit, Output,EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Input() UserFromHomeComponents :any;
  @Output() cancelRegister = new EventEmitter();
  model : any ={};
  loggedIn : boolean = false;
  constructor(public accountService:AccountService, private toastr : ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    //console.log(this.model);
    this.accountService.register(this.model).subscribe(response=>
      {
        console.log(response);
        this.cancel();
      },error=>{
        console.log(error);
        this.toastr.error(error.error);
      });
  
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log("Cancelled--");
  }


}
