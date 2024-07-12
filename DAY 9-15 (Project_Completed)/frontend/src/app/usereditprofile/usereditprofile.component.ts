import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from '../Helper/ValidateForm';
import { ContactUs } from '../model/user.model';
import { AdminloginService } from '../service/adminlogin.service';
import { ClientService } from '../service/client.service';
// import { ImgbbService } from 'src/environments/imgbb.config';
declare var window: any;

@Component({
  selector: 'app-usereditprofile',
  templateUrl: './usereditprofile.component.html',
  styleUrls: ['./usereditprofile.component.css']
})
export class UsereditprofileComponent implements OnInit {
  contactUsModal: any;
  loginUserId: any;
  loginDetail: any;
  loginName: any;
  loginUserDetails: any;
  countryList: any[] = [];
  cityList: any[] = [];
  isFileUpload = false;
  userImage: any = '';
  formData = new FormData();
  userProfileForm: FormGroup;
  userId: any;
  editData: any;

  constructor(
    public service: ClientService,
    private loginService: AdminloginService,
    private router: Router,
    private toast: NgToastService,
    public fb: FormBuilder,
    // private imgbbService: ImgbbService,
    public activateRouter: ActivatedRoute
  ) {
    this.userId = this.activateRouter.snapshot.paramMap.get('userId');
  }

  ngOnInit(): void {
    this.loginService.getCurrentUser().subscribe((data: any) => {
      this.loginDetail = this.loginService.getUserDetail();
      data == null ? (this.loginUserId = this.loginDetail.userId) : (this.loginUserId = data.userId);
      data == null ? (this.loginName = this.loginDetail.fullName) : (this.loginName = data.fullName);
    });

    this.UserFormCheckValid();
    this.loginUserDetailByUserId(this.loginUserId);

    this.FetchData(this.userId);

    this.CountryList();

    this.contactUsModal = new window.bootstrap.Modal(document.getElementById('contactUsModal'));
  }

  CountryList() {
    this.service.CountryList().subscribe(
      (data: any) => {
        if (data) {
          this.countryList = data.map((item: any) => ({
            value: item.id,
            text: item.countryName
          }));
        } else {
          this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
        }
      }
    );
  }

  CityList(countryId: any) {
    countryId = countryId.target.value;
    this.service.CityList(countryId).subscribe(
      (data: any) => {
        if (data) {
          this.cityList = data.map((item: any) => ({
            value: item.id,
            text: item.cityName
          }));
        } else {
          this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
        }
      }
    );
  }

  loginUserDetailByUserId(id: any) {
    this.service.LoginUserDetailById(id).subscribe(
      (data: any) => {
        if (data) {
          this.loginUserDetails = data;
        } else {
          this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
        }
      },
      (err) => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 })
    );
  }

  async OnSelectImage(event: any) {
    if (event.target.files && event.target.files[0]) {
      const selectedFile = event.target.files[0];
      try {
        // const response = await this.imgbbService.uploadImage(selectedFile).toPromise();
        // this.userImage = response.data.url;
        this.isFileUpload = true;
      } catch (error) {
        this.toast.error({ detail: 'ERROR', summary: 'Error uploading image', duration: 3000 });
      }
    }
  }

  UserFormCheckValid() {
    this.userProfileForm = this.fb.group({
      id: [0],
      name: [this.loginDetail.firstName, Validators.compose([Validators.required, Validators.maxLength(16)])],
      surname: [this.loginDetail.lastName, Validators.compose([Validators.required, Validators.maxLength(16)])],
      employeeId: [''],
      manager: [''],
      title: ['', Validators.compose([Validators.maxLength(255)])],
      department: ['', Validators.compose([Validators.maxLength(16)])],
      myProfile: [null, Validators.compose([Validators.required])],
      whyIVolunteer: [''],
      countryId: [null, Validators.compose([Validators.required])],
      cityId: [null, Validators.compose([Validators.required])],
      avilability: [''],
      linkdInUrl: [''],
      userImage: ['', Validators.compose([Validators.required])],
      userId: ['']
    });
  }

  FetchData(id: any) {
    this.service.GetUserProfileDetailById(id).subscribe(
      (response: any) => {
        if (response) {
          const userData = response.user;
          const userDetails = response.userDetails;
         
          if (userData && userDetails) {
            this.editData = userDetails;
            this.userImage = this.editData.userImage;
            this.userProfileForm.patchValue({
              id: userDetails.id,
              name: userData.firstName,
              surname: userData.lastName,
              employeeId: userDetails.employeeId,
              manager: userDetails.manager,
              title: userDetails.title,
              department: userDetails.department,
              myProfile: userDetails.myProfile,
              whyIVolunteer: userDetails.whyIVolunteer,
              countryId: userDetails.countryId,
              cityId: userDetails.cityId,
              availability: userDetails.availability,
              linkdInUrl: userDetails.linkdInUrl,
              userId: userDetails.userId,
              userImage : userDetails.userImage
            
            });
          } else {
            this.toast.error({ detail: 'ERROR', summary: 'User data not found', duration: 3000 });
          }
        } else {
          this.toast.error({ detail: 'ERROR', summary: 'Empty response received', duration: 3000 });
        }
      },
      (err) => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 })
    );
  }
  

  async OnSubmit() {
    let imageUrl = '';
    const formValue = this.userProfileForm.value;
  
    // Prepare the payload
    const payload = {
      user: {
        id: formValue.userId, // Assuming userId is the user's id
        firstName: formValue.name,
        lastName: formValue.surname,
     

        // Add other user properties as needed
      },
      userDetails: {
        id: formValue.id,
        userId: formValue.userId,
        name: formValue.name,
        surname: formValue.surname,
        employeeId: formValue.employeeId,
        manager: formValue.manager,
        title: formValue.title,
        department: formValue.department,
        myProfile: formValue.myProfile,
        whyIVolunteer: formValue.whyIVolunteer,
        countryId: formValue.countryId,
        cityId: formValue.cityId,
        availability: formValue.availability,
        linkdInUrl: formValue.linkdInUrl,
        mySkills: formValue.mySkills,
        userImage: formValue.userImage,
        status: true
      }
    };
  
    // Check if the form is valid
    if (this.userProfileForm.valid) {
  
      if (this.isFileUpload) {
        payload.userDetails.userImage = this.userImage;
      } else {
        payload.userDetails.userImage = this.editData.userImage;
      }
  
      // Send the PUT request
      this.service.LoginUserProfileUpdate(payload)
        .subscribe((res: any) => {
          if (res) {
            this.toast.success({ detail: 'SUCCESS', summary: "Profile Updated", duration: 3000 });
            setTimeout(() => {
              this.router.navigate(['home']);
            }, 1000);
          } else {
            this.toast.error({ detail: 'ERROR', summary: res.message, duration: 3000 });
          }
        },
        (err) => {
          this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 });
        });
    } else {
      // If form is invalid, display validation errors
      ValidateForm.ValidateAllFormFields(this.userProfileForm);
    }
  }
  
      
        contactUs: ContactUs = new ContactUs();
      
        OnSubmitContactUs(form: any) {
          form.value.userId = this.contactUs.userId;
          form.value.name = this.contactUs.name;
          form.value.emailAddress = this.contactUs.emailAddress;
          this.service.ContactUs(form.value).subscribe(
            (data: any) => {
              if (data.result == 1) {
                this.toast.success({ detail: 'SUCCESS', summary: data.data, duration: 3000 });
                setTimeout(() => {
                  form.value.subject = '';
                  form.value.message = '';
                  this.CloseContactUsModal();
                }, 1000);
              } else {
                this.toast.error({ detail: 'ERROR', summary: data.message, duration: 3000 });
              }
            },
            (err) => this.toast.error({ detail: 'ERROR', summary: err.message, duration: 3000 })
          );
        }
      
        OnCancel() {
          this.router.navigate(['/']);
        }
      
        OpenContactUsModal() {
          this.contactUsModal.show();
        }
      
        CloseContactUsModal() {
          this.contactUsModal.hide();
        }
      }