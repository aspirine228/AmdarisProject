import { Injectable } from '@angular/core';
import { News } from '../_models/News/news';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CreateNews } from '../_models/News/createnews';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  httpOptions ={ headers: new HttpHeaders({})};
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }

  getOptions(){
    const httpOptions ={ headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: "Bearer "+ localStorage.getItem("accessToken")! }) };
    this.httpOptions=httpOptions;
  }
  getNews(): Observable<News[]> {
    
    return this.http.get<News[]>(this.baseUrl + 'news', this.httpOptions);
  }

  getCurrentNews(id:number):Observable<News>{
    this.getOptions();
    return this.http.get<News>(this.baseUrl+'news/'+id, this.httpOptions);
  }

  createNews(news:CreateNews){
    this.getOptions();
    return this.http.post(this.baseUrl+'news/create/',news, this.httpOptions);
  }

  updateNews(news:News): Observable<News> {
    this.getOptions();
    return this.http.put<News>(this.baseUrl + 'news/'+news.id,news, this.httpOptions);
  }

  deleteNews(id:number){
    this.getOptions();
    return this.http.delete(this.baseUrl+'news/'+id,this.httpOptions);
  }
}
