import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams, JsonpClientBackend} from '@angular/common/http'
import {UserForLogin} from '../_models/Account/user-for-login'
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { BearerToken } from '../_models/Account/bearer-token';
import { UserForRegister } from '../_models/Account/user-for-register';
import { StringMap } from '@angular/compiler/src/compiler_facade_interface';
import { UserRole } from '../_models/Account/user-role';
import { UserId } from '../_models/Account/user-id';
import { CompanyForRegister } from '../_models/Account/company-for-register';
import { retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  
  baseUrl=environment.apiUrl + 'account/'
  constructor(private http:HttpClient) { }
  username!:string;
  
  httpOptions ={ headers: new HttpHeaders({})};

  isLoggedIn():boolean{
    const token=localStorage.getItem('accessToken');
    
    return token ? true:false;
  }

  login(userForLoginDto:UserForLogin):Observable<BearerToken>
  {
    
    this.username=userForLoginDto.userName;
    
    return this.http.post<BearerToken>(this.baseUrl+'login/',userForLoginDto);
  }

  registerCompany(user:CompanyForRegister){
    return this.http.post(this.baseUrl+'register/company/',user);

  }
  register(user:UserForRegister){
    return this.http.post(this.baseUrl+'register/',user);

  }
  getOptions(){
    const httpOptions ={ headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: "Bearer "+ localStorage.getItem("accessToken")! }) };
    this.httpOptions=httpOptions;
  }
  getId(userName:string):Observable<UserId>{
    this.getOptions();
    
    return this.http.get<UserId>(this.baseUrl+userName, this.httpOptions);
  }
  getRole(id:number):Observable<UserRole>{
    this.getOptions();
    
    return  this.http.get<UserRole>(this.baseUrl+'role/'+id ,this.httpOptions);
  }
  deleteUserById(id:number){
    this.getOptions();
    return this.http.delete(this.baseUrl+'delete/'+id,this.httpOptions);
  }

 updateUserPhone(user:UserId){
  this.getOptions();
  return this.http.patch<UserId>(this.baseUrl+'Update/',user,this.httpOptions);
 }
}
