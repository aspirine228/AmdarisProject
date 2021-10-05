import { TOUCH_BUFFER_MS } from '@angular/cdk/a11y/input-modality/input-modality-detector';
import { templateJitUrl } from '@angular/compiler';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit, OnChanges } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserId } from 'src/app/_models/Account/user-id';
import { UserRole } from 'src/app/_models/Account/user-role';
import { AccountService } from 'src/app/_service/account.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {

  role!:UserRole;
  roleName!:string;
  phoneNumber!:string;
  username!:string;
  userId!:UserId;
  id!:number;
  constructor(private router: Router,
    private accountService:AccountService) { 
    this.username=  this.accountService.username; 
    }
ngOnChanges(){
  this.username=this.accountService.username;
 // this.username!=localStorage.getItem("username");
}
  ngOnInit()
   {
     this.username=this.accountService.username;
    this.getid();
    
    
   }

   getid(){
    this.accountService.getId(this.username).subscribe((userId:UserId)=>
    {               
      this.getrole(userId.id);
      this.id=userId.id;      
      this.phoneNumber=userId.phoneNumber;  
    }
    );
       
  }

 getrole(id:number){
  
     this.accountService.getRole(id).subscribe((role:UserRole) => {
      this.roleName=role.roleName;   
  }); 
 }



  logout(): void {
    localStorage.removeItem('accessToken');
    this.router.navigate(['login']);
  }
}
