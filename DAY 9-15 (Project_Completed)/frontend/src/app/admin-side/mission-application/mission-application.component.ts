import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';

@Component({
  selector: 'app-mission-application',
  templateUrl: './mission-application.component.html',
  styleUrls: ['./mission-application.component.css']
})
export class MissionApplicationComponent implements OnInit {
  applicationList:any[]=[];
  searchText:any="";
  page: number = 1;
  itemsPerPages: number = 5;
  applicationId:any;
  constructor(public service:AdminsideServiceService,private toast:NgToastService,private route:Router) { }

  ngOnInit(): void {
    this.FetchMissionApplicationList();
  }

  getStatus(status) {
    return status ? 'Approve' : 'Pending';
  }

  FetchMissionApplicationList(){
    this.service.MissionApplicationList().subscribe((data:any)=>{
      if(data)
      {
          this.applicationList = data;
      }
      else
      {
        this.toast.error({detail:"ERROR",summary:data.message,duration:3000});
      }
    },err=>this.toast.error({detail:"ERROR",summary:err.message,duration:3000}));
  }

  ApproveMissionApplication(id: number) {
    this.service.MissionApplicationApprove(id).subscribe(
      (data: any) => {
        if (data.success) {
          this.toast.success({ detail: "SUCCESS", summary: data.message, duration: 3000 });
          setTimeout(() => {
            this.FetchMissionApplicationList();
          }, 2000);
        } else {
          this.toast.error({ detail: "ERROR", summary: data.message, duration: 3000 });
        }
      },
      (err) => {
        this.toast.error({ detail: "ERROR", summary: err.message || "Server error", duration: 3000 });
      }
    );
  }
  
  DeleteMissionApplication(id: number) {
    this.service.MissionApplicationDelete(id).subscribe(
      (data: any) => {
        if (data.success) {
          this.toast.success({ detail: "SUCCESS", summary: data.message, duration: 3000 });
          setTimeout(() => {
            this.FetchMissionApplicationList();
          }, 2000);
        } else {
          this.toast.error({ detail: "ERROR", summary: data.message, duration: 3000 });
        }
      },
      (err) => {
        this.toast.error({ detail: "ERROR", summary: err.message || "Server error", duration: 3000 });
      }
    );
  }
  
}