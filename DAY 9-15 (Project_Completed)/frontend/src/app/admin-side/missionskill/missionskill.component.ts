import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';
declare var window: any;
@Component({
  selector: 'app-missionskill',
  templateUrl: './missionskill.component.html',
  styleUrls: ['./missionskill.component.css']
})
export class MissionskillComponent implements OnInit {
  missionSkillList: any[] = [];
  deleteSkillModal: any = false; // Initialize modal state
  page: number = 1;
  itemsPerPage: number = 10;
  searchText: string = '';
  skillId: any;

  constructor(
    private service: AdminsideServiceService,
    private router: Router,
    private toast: NgToastService
  ) {}

  ngOnInit(): void {
    this.getMissionSkillList();
    this.deleteSkillModal = new window.bootstrap.Modal(
      document.getElementById('removeMissionSkillModal')
    );
  }

  getMissionSkillList(): void {
    this.service.MissionSkillList().subscribe(
      (data: any) => {
        console.log(data);
        if (data) {
          this.missionSkillList = data;
        } else {
          this.toast.error({
            detail: 'ERROR',
            summary: data.message,
            duration: 3000
          });
        }
      },
      (err) =>
        this.toast.error({
          detail: 'ERROR',
          summary: err.message,
          duration: 3000
        })
    );
  }

  routeToEditMissionSkill(skillId: number): void {
    this.router.navigate(['admin/updateMissionSkill', skillId]);
  }

 openDeleteModal(skillId: number): void {
  this.skillId = skillId;
  this.deleteSkillModal.show();
}

closeDeleteModal(): void {
  this.skillId = null;
  this.deleteSkillModal.hide();
}

  deleteMissionSkill(): void {
    this.service.DeleteMissionSkill(this.skillId).subscribe(
      (data: any) => {
   
        if (data) {
          this.toast.success({
            detail: 'SUCCESS',
            summary: "Deleted Sucessfully",
            duration: 3000
          });
          this.closeDeleteModal();
          this.getMissionSkillList(); // Refresh mission skill list
        } else {
          this.toast.error({
            detail: 'ERROR',
            summary: "Something went wrong",
            duration: 3000
          });
        }
      },
      (err) =>
        this.toast.error({
          detail: 'ERROR',
          summary: err.message,
          duration: 3000
        })
    );
  }
}
