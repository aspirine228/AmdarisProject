import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../_service/account.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private accountService:AccountService,private router:Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree 
    {
      if(this.accountService.isLoggedIn())
      {
        return true;
      }
      this.router.navigate(['login'],{queryParams:{returnUrl:state.url}});
      return false;
  } 
  
}
