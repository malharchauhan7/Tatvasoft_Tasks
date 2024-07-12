import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from 'src/app/Helper/ValidateForm';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';

@Component({
  selector: 'app-update-mission-skill',
  templateUrl: './update-mission-skill.component.html',
  styleUrls: ['./update-mission-skill.component.css']
})
export class UpdateMissionSkillComponent implements OnInit {
  updateMissionSkillForm: FormGroup;
  skillId: any;
  editData: any;

  constructor(
    public fb: FormBuilder,
    public router: Router,
    public toast: NgToastService,
    public service: AdminsideServiceService,
    public activateRoute: ActivatedRoute
  ) {
    this.skillId = this.activateRoute.snapshot.paramMap.get('Id');
  }

  ngOnInit(): void {
    this.MissionSkillFormValidate();
    if (this.skillId != null) {
      this.FetchDataById(this.skillId);
    }
  }

  MissionSkillFormValidate() {
    this.updateMissionSkillForm = this.fb.group({
      id: [0],
      skillName: ['', Validators.compose([Validators.required])],
      status: ['', Validators.compose([Validators.required])]
    });
  }

  FetchDataById(id: any) {
    this.service.MissionSkillById(id).subscribe((data: any) => {
      if (data) {
        console.log(data)
        this.editData = data;
        this.updateMissionSkillForm.patchValue(this.editData);
      } else {
        this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
      }
    }, err => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 }))
  }

  OnSubmit() {
    if (this.updateMissionSkillForm.valid) {
      let value = this.updateMissionSkillForm.value;
      this.UpdateData(value);
    } else {
      ValidateForm.ValidateAllFormFields(this.updateMissionSkillForm);
    }
  }

  UpdateData(value: any) {
    this.service.UpdateMissionSkill(value).subscribe((data: any) => {
      if (data) {
        this.toast.success({ detail: 'SUCCESS', summary: "Updated Successfully", duration: 3000 });
        setTimeout(() => {
          this.router.navigate(['admin/missionSkill']);
        }, 1000);
      } else {
        this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
      }
    }, err => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 }))
  }

  OnCancel() {
    this.router.navigate(['admin/missionSkill']);
  }
}