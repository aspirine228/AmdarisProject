import { Component, OnInit } from '@angular/core';
import { FormGroup,FormBuilder,Validators,ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-company-register',
  templateUrl: './company-register.component.html',
  styleUrls: ['./company-register.component.css']
})
export class CompanyRegisterComponent implements OnInit {

  form!:FormGroup;
  loading=false;
  submitted=false;
  
    constructor(
      private formBuilder:FormBuilder,
      private route:ActivatedRoute,
      private router:Router,
      private accountService:AccountService,
    ) { }
  
    ngOnInit(): void {
      this.form=this.formBuilder.group({
        fullName: ['',Validators.required],
        companyname: ['',Validators.required],
        password: ['',[Validators.required,Validators.minLength(6)]],
        companyemail: ['',[Validators.required]]
  
      });
    }
    get f(){return this.form.controls;}
  
    onSubmit(){
      this.submitted=true;
      if(this.form.invalid){
        return;
      }
  
      this.loading=true;
  
      this.accountService.registerCompany(this.form.value)
      .pipe(first())
      .subscribe(
        data=>{
          this.router.navigate(['../login',{relativeTo:this.route}]);
        },
        error=>{
          this.loading=false;
        }
        
      );
    }


}
