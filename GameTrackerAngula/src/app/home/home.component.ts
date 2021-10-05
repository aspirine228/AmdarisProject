import { Component, OnInit } from '@angular/core';
import { Game } from 'src/app/_models/Game/game';
import { GameService } from 'src/app/_service/game.service';
import { News } from '../_models/News/news';
import { AccountService } from '../_service/account.service';
import { NewsService } from '../_service/news.service';
import { FeedbackService } from '../_service/feedback.service';
import { Feedback } from '../_models/FeedBack/feedback';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  gamelist!:Game[];
  newsList!:News[];
  feedBackList!:Feedback[];
  length!:number;
  bool!:boolean;
  username!:string;
  constructor(private gameService:GameService,
    private feedBackService:FeedbackService,
    
    private newsService:NewsService,
    private accountService:AccountService
  ) {this.username!=localStorage.getItem('username'); }

  ngOnInit(): void {
    
    if(this.accountService.isLoggedIn()){
      this.bool=true;
      this.username=this.accountService.username;
      localStorage.setItem('username',this.username);
    }else{
      this.bool=false;
    }
    this.loadGamesFromApi();
    this.loadNews();
    this.loadFeedBacks();
  }

  loadNews(){
    this.newsService.getNews().subscribe((list:News[])=>{
      this.newsList=list;
    });

  }

  loadFeedBacks(){
this.feedBackService.getFeedBacks().subscribe((list:Feedback[])=>{
  this.feedBackList=list;
});
  }
  loadGamesFromApi() {
   
    this.gameService.getGames()
      .subscribe((list: Game[]) => {
        
        this.gamelist = list ;
        this.length= list.length;

      });
    }
}
