import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/environment.prod';
import { City } from './city';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';


@Component({
  selector: 'app-cities',
  templateUrl: './cities.component.html',
  styleUrls: ['./cities.component.scss']
})


export class CitiesComponent implements OnInit {
  public displayedColumns: string[] = ['id', 'name', 'lat', 'lon'];
  public cities!: MatTableDataSource<City>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private http: HttpClient) {
  }


  ngOnInit() {
    this.http.get<City[]>(environment.baseUrl + 'api/Cities')
      .subscribe(result => {
        this.cities = new MatTableDataSource<City>(result);
        this.cities.paginator = this.paginator;
      }, error => console.error(error));
  }
}
