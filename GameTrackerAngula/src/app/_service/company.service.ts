import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Company } from '../_models/Company/company';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  httpOptions ={ headers: new HttpHeaders({})};
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }

 getOptions(){
    const httpOptions ={ headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: "Bearer "+ localStorage.getItem("accessToken")! }) };
    this.httpOptions=httpOptions;
  }
 
  getCompanies(): Observable<Company[]> {
    this.getOptions();
    return this.http.get<Company[]>(this.baseUrl + 'companies', this.httpOptions);
  }

  getCompanyByUserName(userName:string): Observable<Company> {
    this.getOptions();
    return this.http.get<Company>(this.baseUrl + 'companies'+ userName, this.httpOptions);
  }
}
