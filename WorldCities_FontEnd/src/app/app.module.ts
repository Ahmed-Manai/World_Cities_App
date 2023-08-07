import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HomeComponentComponent } from './home-component/home-component.component';
import { NavMenuComponentComponent } from './nav-menu-component/nav-menu-component.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { CitiesComponent } from './cities/cities.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponentComponent,
    NavMenuComponentComponent,
    CitiesComponent
  ],
  imports: [
    BrowserModule, HttpClientModule, AppRoutingModule, BrowserAnimationsModule,
    MatButtonModule, MatIconModule, MatToolbarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
