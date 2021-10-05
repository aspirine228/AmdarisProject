import { Component, OnInit } from '@angular/core';
import { UserForLogin } from '../_models/Account/user-for-login';
import { AccountService } from '../_service/account.service';
import { BearerToken } from '../_models/Account/bearer-token';
import { FormGroup,FormBuilder,Validators,ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { registerLocaleData } from '@angular/common';
import { UserId } from '../_models/Account/user-id';



@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private returnUrl!:string;
  public userLoginForm!:FormGroup;
  public isLogged!:boolean;
  public userId!:UserId;
  constructor(private accountService:AccountService,
    private router:Router,
    private route:ActivatedRoute,
    private formBuilder:FormBuilder) { }

  ngOnInit() 
  {
    this.userLoginForm =this.formBuilder.group(
    {
      userName:['',[Validators.required]],
      password:['',[Validators.required]]
    });
    this.returnUrl=this.route.snapshot.queryParams['home'] || '/admin';
  }
  login(){
    const userLogin:UserForLogin={
      ...this.userLoginForm.value
    };
    this.accountService.login(userLogin).subscribe((bearerToken:BearerToken)=>{
      localStorage.setItem('accessToken',bearerToken.accessToken);
      localStorage.setItem("username",userLogin.userName);
      this.router.navigate([this.returnUrl]);
    });
   
        this.accountService.getOptions();
  
}
 

} 
