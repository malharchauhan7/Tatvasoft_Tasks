import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/Helper/ValidateForm';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';

@Component({
  selector: 'app-update-mission-theme',
  templateUrl: './update-mission-theme.component.html',
  styleUrls: ['./update-mission-theme.component.css']
})
export class UpdateMissionThemeComponent implements OnInit {
  updateMissionThemeForm: FormGroup;
  themeId: any;
  editData: any;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private toast: NgToastService,
    private service: AdminsideServiceService,
    private activateRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.initMissionThemeForm();
    this.themeId = this.activateRoute.snapshot.paramMap.get('Id');
    if (this.themeId) {
      this.FetchDataById(this.themeId);
    }
  }

  initMissionThemeForm() {
    this.updateMissionThemeForm = this.fb.group({
      id: [0],
      themeName: ['', Validators.required],
      status: ['', Validators.required]
    });
  }


  FetchDataById(id: any) {
    this.service.MissionThemeById(id).subscribe((data: any) => {
      if (data) {
        console.log(data)
        this.editData = data;
        this.updateMissionThemeForm.patchValue(this.editData);
      } else {
        this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
      }
    }, err => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 }))
  }

  onSubmit() {
    if (this.updateMissionThemeForm.valid) {
      this.updateData(this.updateMissionThemeForm.value);
    } else {
      ValidateForm.ValidateAllFormFields(this.updateMissionThemeForm);
    }
  }

  updateData(value: any) {
    this.service.UpdateMissionTheme(value).subscribe(
      (data: any) => {
        if (data) {
          this.toast.success({ detail: "SUCCESS", summary: "Updated Successfully", duration: 3000 });
          setTimeout(() => {
            this.router.navigate(['admin/missionTheme']);
          }, 1000);
        } else {
          console.log("Error: Response data is null or undefined");
          this.toast.error({ detail: "ERROR", summary: data?.message || "Unknown error", duration: 3000 });
        }
      },
      (error) => {
        console.error("API Error:", error); // Log the error details
        this.toast.error({ detail: "ERROR", summary: error.message || "Error updating data", duration: 3000 });
      }
    );
  }

  onCancel() {
    this.router.navigateByUrl('admin/missionTheme');
  }
}
