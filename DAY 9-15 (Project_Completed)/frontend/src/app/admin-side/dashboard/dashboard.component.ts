import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import dateFormat, { masks } from 'dateformat';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  data: any;
  statistics: any;

  constructor(private http: HttpClient) {
    setInterval(() => {
      const now = new Date();
      this.data = dateFormat(now, "dddd mmmm dS,yyyy, h:MM:ss TT");
    }, 1);
  }

  ngOnInit(): void {
    this.fetchStatistics();
  }

  fetchStatistics(): void {
    this.http.get('https://localhost:7178/api/Statistics')
      .subscribe(
        (response) => {
          this.statistics = response;
          // Handle the response data as needed
          console.log(this.statistics);
        },
        (error) => {
          console.error('Error fetching statistics:', error);
        }
      );
  }
}