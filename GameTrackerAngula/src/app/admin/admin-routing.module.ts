import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminComponent } from './admin/admin.component';
import { CompaniesListComponent } from './companies/companies-list/companies-list.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { FeedbackeditComponent } from './feedback/feedbackedit/feedbackedit.component';
import { GamersListComponent } from './gamers/gamers-list/gamers-list.component';
import { GameslistComponent } from './games/gameslist/gameslist.component';
import { NewsComponent } from './news/news.component';
import { NewseditComponent } from './news/newsedit/newsedit.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
{
  path:'',
  component:AdminComponent,
  children:[
    {
      path:'',
      children:[
         {path:'dashboard',component:DashboardComponent },
         {path:'games',component:GameslistComponent },
         {path:'companies',component:CompaniesListComponent },
         {path:'gamers',component:GamersListComponent },
         {path:'news',component:NewsComponent },
         {path:'newsedit/:id',component:NewseditComponent},
         {path:'feedback',component:FeedbackComponent },
         {path:'feedbackedit/:id', component:FeedbackeditComponent},
         {path:'profile',component:ProfileComponent },
       // {path:'',component: }

      ]
    }
  ]
}


];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
