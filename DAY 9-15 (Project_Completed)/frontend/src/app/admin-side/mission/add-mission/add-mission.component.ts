
import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';
import { NgToastService } from 'ng-angular-popup';
import { ImgbbService } from 'src/environments/imgbb.config';

@Component({
  selector: 'app-add-mission',
  templateUrl: './add-mission.component.html',
  styleUrls: ['./add-mission.component.css']
})
export class AddMissionComponent implements OnInit {
  addMissionForm: FormGroup;
  endDateDisabled: boolean = true;
  regDeadlineDisabled: boolean = true;
  formValid: boolean;
  countryList: any[] = [];
  cityList: any[] = [];
  missionThemeList: any[] = [];
  missionSkillList: any[] = [];
  selectedImage: File | null = null;
  uploadedImageUrl: string = '';

  constructor(
    public fb: FormBuilder,
    public service: AdminsideServiceService,
    public toastr: ToastrService,
    public router: Router,
    public datePipe: DatePipe,
    private toast: NgToastService,
    private imgbbService: ImgbbService
  ) { }

  ngOnInit(): void {
    this.addMissionFormValid();
    this.setStartDate();
    this.CountryList();
    this.GetMissionSkillList();
    this.GetMissionThemeList();
  }

  addMissionFormValid() {
    this.addMissionForm = this.fb.group({
      countryId: [null, Validators.compose([Validators.required])],
      cityId: [null, Validators.compose([Validators.required])],
      missionTitle: [null, Validators.compose([Validators.required])], // Add this line
      missionDescription: [null, Validators.compose([Validators.required])],
      startDate: [null, Validators.compose([Validators.required])],
      endDate: [null, Validators.compose([Validators.required])],
      missionThemeId: [null, Validators.compose([Validators.required])],
      missionSkillId: [null, Validators.compose([Validators.required])],
      missionImages: [null, Validators.compose([Validators.required])],
      totalSheets: [null, Validators.compose([Validators.required])]
    });
  }
  

  get countryId() { return this.addMissionForm.get('countryId') as FormControl; }
  get cityId() { return this.addMissionForm.get('cityId') as FormControl; }
  get missionTitle() { return this.addMissionForm.get('missionTitle') as FormControl; }
  get missionDescription() { return this.addMissionForm.get('missionDescription') as FormControl; }
  get startDate() { return this.addMissionForm.get('startDate') as FormControl; }
  get endDate() { return this.addMissionForm.get('endDate') as FormControl; }
  get missionThemeId() { return this.addMissionForm.get('missionThemeId') as FormControl; }
  get missionSkillId() { return this.addMissionForm.get('missionSkillId') as FormControl; }
  get missionImages() { return this.addMissionForm.get('missionImages') as FormControl; }
  get totalSheets() { return this.addMissionForm.get('totalSheets') as FormControl; }

  setStartDate() {
    const today = new Date();
    const todayString = today.toISOString().split('T')[0];

    this.addMissionForm.patchValue({
      startDate: todayString
    });
    this.endDateDisabled = false;
    this.regDeadlineDisabled = false;
  }

  CountryList() {
    this.service.CountryList().subscribe((data: any) => {
      if (data) {
        this.countryList = data.map((item: any) => ({
          value: item.id,
          text: item.countryName
        }));
      } else {
        this.toast.error({ detail: "ERROR", summary: data.message, duration: 3000 });
      }
    });
  }

  CityList(countryId: any) {
    countryId = countryId.target.value;
    this.service.CityList(countryId).subscribe((data: any) => {
      if (data) {
        this.cityList = data.map((item: any) => ({
          value: item.id,
          text: item.cityName
        }));
      } else {
        this.toast.error({ detail: "ERROR", summary: data.message, duration: 3000 });
      }
    });
  }

  GetMissionSkillList() {
    this.service.GetMissionSkillList().subscribe((data: any) => {
      if (data) {
        this.missionSkillList = data.map((item: any) => ({
          value: item.id,
          text: item.skillName
        }));
      } else {
        this.toast.error({ detail: "ERROR", summary: data.message, duration: 3000 });
      }
    }, err => this.toast.error({ detail: "ERROR", summary: err.message, duration: 3000 }))
  }

  GetMissionThemeList() {
    this.service.GetMissionThemeList().subscribe((data: any) => {
      if (data) {

        this.missionThemeList = data.map((item: any) => ({
          value: item.id,
          text: item.themeName
        }));

      } else {
        this.toast.error({ detail: "ERROR", summary: data.message, duration: 3000 });
      }
    }, err => this.toast.error({ detail: "ERROR", summary: err.message, duration: 3000 }))
  }

  onImageSelected(event: any) {
    this.selectedImage = event.target.files[0];
  }

  async onUpload() {
    if (this.selectedImage) {
      try {
        const response = await this.imgbbService.uploadImage(this.selectedImage).toPromise();
        this.uploadedImageUrl = response.data.url;
        // Set the uploaded image URL as a string value in the form
        this.addMissionForm.get('missionImages')?.setValue(this.uploadedImageUrl);
      } catch (error) {
        this.toast.error({ detail: "ERROR", summary: 'Error uploading image', duration: 3000 });
      }
    }
  }
  
  createMissionDto(): MissionDto {
    const formValue = this.addMissionForm.value;
    const selectedCountry = this.countryList.find(country => country.value === formValue.countryId);
    const selectedCity = this.cityList.find(city => city.value === formValue.cityId);
  
    const missionDto: MissionDto = {
      id: 0,
      countryId: formValue.countryId,
      cityId: formValue.cityId,
      missionDescription: formValue.missionDescription,
      totalSheets: formValue.totalSheets,
      startDate: new Date(formValue.startDate),
      endDate: new Date(formValue.endDate),
      missionImages: formValue.missionImages, 
      missionSkillId: Array.isArray(formValue.missionSkillId) ? formValue.missionSkillId.join(',') : formValue.missionSkillId,
      missionThemeId: Array.isArray(formValue.missionSkillId) ? formValue.missionSkillId.join(',') : formValue.missionSkillId,
      missionTitle: formValue.missionTitle,
      missionOrganisationName: null, 
      missionOrganisationDetail: null, 
      countryName: selectedCountry ? selectedCountry.text : null,
      cityName: selectedCity ? selectedCity.text : null,
    };
    return missionDto;
  }
  

  async OnSubmit() {
    this.formValid = true;
    if (this.formValid) {
      const missionDto = this.createMissionDto();
      this.service.AddMission(missionDto).subscribe(
        (data: any) => {
          this.toast.success({ detail: "SUCCESS", summary: "Mission Posted Successfully", duration: 3000 });
          setTimeout(() => {
            this.router.navigate(['admin/mission']);
          }, 1000);
        },
        (error) => {
          this.toast.error({ detail: "ERROR", summary: "Something went wrong", duration: 3000 });
          console.error('Error adding mission:', error);
        }
      );
      this.formValid = false;
    }
  }
  OnCancel() {
    this.router.navigateByUrl('admin/mission');
  }
}
export interface MissionDto {
  id: number;
  countryId: number;
  cityId: number;
  missionDescription: string;
  totalSheets?: number;
  startDate: Date;
  endDate: Date;
  missionImages?: string;
  missionSkillId?: string;
  missionThemeId?: string;
  missionTitle: string;
  missionOrganisationName: string | null;
  missionOrganisationDetail: string | null;
  countryName: string | null;
  cityName: string | null;
}
