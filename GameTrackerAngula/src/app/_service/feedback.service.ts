import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Feedback } from '../_models/FeedBack/feedback';
import { identifierModuleUrl } from '@angular/compiler';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  httpOptions ={ headers: new HttpHeaders({})};
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }

  getOptions(){
    const httpOptions ={ headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: "Bearer "+ localStorage.getItem("accessToken")! }) };
    this.httpOptions=httpOptions;
  }

  getFeedBacks():Observable<Feedback[]>{
    return this.http.get<Feedback[]>(this.baseUrl + 'feedback', this.httpOptions);
  }
  getFeedBacksForUser(id:number):Observable<Feedback[]>{
    this.getOptions();
    return this.http.get<Feedback[]>(this.baseUrl+'feedback/user/'+id,this.httpOptions);
  }
  createFeedBack(feedBack:Feedback){
    this.getOptions();
    return this.http.post(this.baseUrl+'feedback',feedBack,this.httpOptions);
  }
  deleteUserFeedBack(id:number){
    this.getOptions();
    return this.http.delete(this.baseUrl+'feedback/'+id,this.httpOptions);
  }
  updateUserFeedBack(feedBack:Feedback){
    this.getOptions();
    return this.http.patch(this.baseUrl+'feedback/Update/',feedBack,this.httpOptions);
  }
  getFeedBack(id:number):Observable<Feedback>{
    this.getOptions();
    return this.http.get<Feedback>(this.baseUrl+'feedback/'+id,this.httpOptions);
  }
}
