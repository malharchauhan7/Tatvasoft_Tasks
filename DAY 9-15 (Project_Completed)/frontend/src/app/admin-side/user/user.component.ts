import { Component, OnInit } from '@angular/core';
import { NgToastService } from 'ng-angular-popup';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';
import { UserDetail } from '../../model/user.model';
declare var window: any;

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  page: number = 1;
  itemsPerPages: number = 10;
  searchText: any = '';
  userDetailList: UserDetail[] = [];
  deleteModal: any;
  userDetailId: any;

  constructor(private service: AdminsideServiceService, private toast: NgToastService) { }

  ngOnInit(): void {
    this.FetchUserDetailList();
    this.deleteModal = new window.bootstrap.Modal(
      document.getElementById('removeMissionModal')
    );
  }

  FetchUserDetailList() {
    this.service.getUserDetails().subscribe(
      (data: UserDetail[]) => {
        this.userDetailList = data;
      },
      err => this.toast.error({ detail: "ERROR", summary: err.error.message, duration: 3000 })
    );
  }

  CloseRemoveMissionModal() {
    this.deleteModal.hide();
  }

  DeleteUserDetail() {
    this.service.deleteUserDetail(this.userDetailId).subscribe(
      (data: any) => {
        if (data === null || data === undefined) {
          // Successful deletion, data is null/undefined
          this.toast.success({
            detail: "SUCCESS",
            summary: "User detail deleted successfully",
            duration: 3000
          });
  
          // Close the modal
          const modalElement = document.getElementById('removeMissionModal');
          if (modalElement) {
            const modal = window.bootstrap.Modal.getInstance(modalElement);
            modal.hide();
          }
  
          // Update the table
          this.FetchUserDetailList();
        } else {
          // Error scenario
          this.toast.error({
            detail: "ERROR",
            summary: data.message || 'An error occurred while deleting the user detail',
            duration: 3000
          });
        }
      },
      err => this.toast.error({
        detail: "ERROR",
        summary: err.error.message || 'An error occurred while deleting the user detail',
        duration: 3000
      })
    );
  }
}