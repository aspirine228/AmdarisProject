import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Gamer } from '../_models/Gamer/gamer';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GamerService {
  httpOptions ={ headers: new HttpHeaders({})};
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }

  getOptions(){
    const httpOptions ={ headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: "Bearer "+ localStorage.getItem("accessToken")! }) };
    this.httpOptions=httpOptions;
  }

  getGamers(): Observable<Gamer[]> {
    this.getOptions();
    return this.http.get<Gamer[]>(this.baseUrl + 'gamers', this.httpOptions);
  }

  getGamersForCompany(companyName:string): Observable<Gamer[]> {
    this.getOptions();
    return this.http.get<Gamer[]>(this.baseUrl + 'gamers/company/'+companyName, this.httpOptions);
  }

  getGamerOfUserPhone(phoneNumber:string):Observable<Gamer>{
    this.getOptions();
    return this.http.get<Gamer>(this.baseUrl+'gamers/gamer/'+phoneNumber,this.httpOptions)
  }

  updateGamer(gamer:Gamer):Observable<Gamer>{
    this.getOptions();
    return this.http.put<Gamer>(this.baseUrl+'gamers/'+gamer.id,gamer,this.httpOptions);
  }
}
