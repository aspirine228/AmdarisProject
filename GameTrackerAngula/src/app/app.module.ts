import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { MaterialModule } from './material/material.module';
import { AppRoutingModule } from './app-routing.module';
import { JwtModule } from '@auth0/angular-jwt';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './_guards/auth.guard';
import { environment } from 'src/environments/environment';
import { importExpr } from '@angular/compiler/src/output/output_ast';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { IfRolesDirective } from './if-roles.directive';
import { CompanyRegisterComponent } from './company-register/company-register.component';
import { AlerComponent } from './aler/aler.component';




@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NotfoundComponent,
    RegisterComponent,
    HomeComponent,
    IfRolesDirective,
    CompanyRegisterComponent,
    AlerComponent,
    
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    JwtModule.forRoot({
      config:{
        tokenGetter:()=>localStorage.getItem('accessToken'),
       // whitelistedDomain: [environment.whitelistedDomainForToken],
        //blacklistedRoutes: [environment.blacklistedRoutes]
      }
    }),
    MatInputModule
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
