import { Component, OnInit } from '@angular/core';
import { UserForLogin } from '../_models/Account/user-for-login';
import { AccountService } from '../_service/account.service';
import { BearerToken } from '../_models/Account/bearer-token';
import { FormGroup,FormBuilder,Validators,ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AlertService } from '../_service/alert.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
form!:FormGroup;
loading=false;
submitted=false;

  constructor(
    private formBuilder:FormBuilder,
    private route:ActivatedRoute,
    private router:Router,
    private accountService:AccountService,
    private alertService : AlertService
  ) { }

  ngOnInit(): void {
    this.form=this.formBuilder.group({
      fullName: ['',Validators.required],
      username: ['',Validators.required],
      password: ['',[Validators.required,Validators.minLength(6)]],
      phoneNumber: ['',[Validators.required,Validators.maxLength(9)]]

    });
  }
  get f(){return this.form.controls;}

  onSubmit(){
    this.submitted=true;
    this.alertService.clear();
    if(this.form.invalid){
      return;
    }

    this.loading=true;

    this.accountService.register(this.form.value)
    .pipe(first())
    .subscribe(
      data=>{
        this.alertService.success('Registration successful', true);
        this.router.navigate(['../login',{relativeTo:this.route}]);
      },
      error=>{
        this.alertService.error(error);
        this.loading=false;
      }
      
    );
  }
}
