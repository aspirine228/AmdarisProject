import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Game } from '../_models/Game/game';
import { Observable } from 'rxjs';
import { PagedResult } from '../_infrastructure/models/PagedResult';
import { PaginatedRequest } from '../_infrastructure/models/PaginatedRequest';
import { gamelist } from '../_models/Game/gameList';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  httpOptions ={ headers: new HttpHeaders({})};
  baseUrl=environment.apiUrl;
  constructor(private http:HttpClient) { }
  list!:Game[];

  getOptions(){
    const httpOptions ={ headers: new HttpHeaders({ 'Content-Type': 'application/json', Authorization: "Bearer "+ localStorage.getItem("accessToken")! }) };
    this.httpOptions=httpOptions;
  }

  getGames(): Observable<Game[]> {   

    return this.http.get<Game[]>(this.baseUrl + 'games');
  }

  getGamesForUser(phoneNumber:string): Observable<Game[]> {   
    this.getOptions();
    return this.http.get<Game[]>(this.baseUrl + 'games/Gamer/'+phoneNumber,this.httpOptions);
  }

  getGamesForCompany(userName:string): Observable<Game[]> {   
    this.getOptions();
    return this.http.get<Game[]>(this.baseUrl + 'games/Company/'+userName, this.httpOptions);
  }


  GetGames2(){
    this.http.get(this.baseUrl+'games').toPromise().then(res=>this.list=res as Game[]);
  }

  getGamesPaged(paginatedRequest: PaginatedRequest): Observable<PagedResult<gamelist>> {
    this.getOptions();
    return this.http.post<PagedResult<gamelist>>(this.baseUrl + 'games/paginated-search', paginatedRequest,this.httpOptions);
  }
}
