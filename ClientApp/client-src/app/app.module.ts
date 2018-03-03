import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StudentComponent } from './student/student.component';
import { ContactComponent } from './contact/contact.component';
import { CurriculumComponent } from './curriculum/curriculum.component';
import { LoginComponent } from './login/login.component';
import { ScheduleComponent } from './schedule/schedule.component';
import { FooterComponent } from './footer/footer.component';

import { DataService } from './shared/dataService';
import { LoginService } from './shared/loginService';
import { AuthInterceptor } from './shared/authInterceptor';
import { AuthGuardService } from './shared/authGuardService';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    StudentComponent,
    ContactComponent,
    CurriculumComponent,
    ScheduleComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'student', component: StudentComponent },
      { path: 'contact', component: ContactComponent },
      { path: 'curriculum', component: CurriculumComponent },
      { path: 'schedule', component: ScheduleComponent },
    ])
  ],
  providers: [
    DataService,
    LoginService, 
    AuthInterceptor, 
    AuthGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
