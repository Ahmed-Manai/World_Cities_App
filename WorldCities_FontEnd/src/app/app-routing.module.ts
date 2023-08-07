import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponentComponent } from './home-component/home-component.component';
import { CitiesComponent } from './cities/cities.component';

const routes: Routes = [
  { path: '', component: HomeComponentComponent, pathMatch: 'full' },
  { path: 'cities', component: CitiesComponent }
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes)
  ],
  exports:[RouterModule]
})
export class AppRoutingModule { }
