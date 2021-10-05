import { Component, OnInit } from '@angular/core';
import { UserId } from 'src/app/_models/Account/user-id';
import { AccountService } from 'src/app/_service/account.service';
import { AdminComponent } from '../admin/admin.component';
import { GamerService } from 'src/app/_service/gamer.service';
import { Gamer } from 'src/app/_models/Gamer/gamer';
import { FormGroup,FormBuilder,Validators,ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  form!:FormGroup;
  loading=false;
  submitted=false;
   userDetails!:UserId;
   userGamerDetails!:Gamer;
  constructor(private accountService:AccountService,
    private admin:AdminComponent,
    private gamerService:GamerService, private formBuilder:FormBuilder,
    private route:ActivatedRoute,
    private router:Router,
    public snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.getUserDetails();
    this.form=this.formBuilder.group({
     
      name: ['',Validators.required],
     
      email:['',Validators.required],
      //password: ['',[Validators.required,Validators.minLength(6)]],
      phoneNumber: ['',[Validators.required,Validators.maxLength(9)]]

    });
   
  }

  get f(){return this.form.controls;}

  getUserDetails(){
    this.accountService.getId(this.admin.username).subscribe((details:UserId)=>{
      this.userDetails=details;
      this.getGamerDetails(details.phoneNumber);
    });
  }

  onSubmit(){
    this.submitted=true;
    if(this.form.invalid){
      return;
    }

    this.loading=true;
    let gamer:Gamer;
    gamer=this.form.value;
    gamer.id=this.userGamerDetails.id;
    this.gamerService.updateGamer(gamer)
    .pipe(first())
    .subscribe(
      data=>{
        this.router.navigate(['./profile',{relativeTo:this.route}]);
      },
      error=>{
        this.loading=false;
      }     
    );
    this.userDetails.phoneNumber=gamer.phoneNumber;
  
    this.accountService.updateUserPhone(this.userDetails).pipe(first())
    .subscribe(
      data=>{
        this.router.navigate(['./admin',{relativeTo:this.route}]);
      },
      error=>{
        this.loading=false;
      }     
    );
  }

  deleteUser(){
    this.accountService.deleteUserById(this.userDetails.id).subscribe(
      () => {       
        this.snackBar.open('The item has been deleted successfully.', 'Close', {
          duration: 1500
        });
      });
  }

  getGamerDetails(phoneNumber:string){

    this.gamerService.getGamerOfUserPhone(phoneNumber).subscribe((gamerDetails:Gamer)=>{
     
      this.userGamerDetails=gamerDetails;
      
    });
  }



}
