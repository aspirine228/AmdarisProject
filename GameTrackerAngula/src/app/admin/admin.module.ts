import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MaterialModule } from '../material/material.module';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminComponent } from './admin/admin.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { GameslistComponent } from './games/gameslist/gameslist.component';
import { CompaniesListComponent } from './companies/companies-list/companies-list.component';
import { GamersListComponent } from './gamers/gamers-list/gamers-list.component';
import { FeedbackComponent } from './feedback/feedback.component';
import { NewsComponent } from './news/news.component';
import { ProfileComponent } from './profile/profile.component';
import { FeedbackeditComponent } from './feedback/feedbackedit/feedbackedit.component';
import { NewseditComponent } from './news/newsedit/newsedit.component';




@NgModule({
  declarations: [AdminComponent, DashboardComponent, GameslistComponent, CompaniesListComponent, GamersListComponent, FeedbackComponent, NewsComponent, ProfileComponent, FeedbackeditComponent, NewseditComponent],
  imports: [
    CommonModule,
    AdminRoutingModule,
    MaterialModule
  ]
})
export class AdminModule { }
