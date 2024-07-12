import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ToastrService } from 'ngx-toastr';
import { AdminloginService } from 'src/app/service/adminlogin.service';
import { AdminsideServiceService } from 'src/app/service/adminside-service.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  registerForm: FormGroup;
  formValid: boolean;

  constructor(
    private fb: FormBuilder,
    private service: AdminloginService,
    private toastr: ToastrService,
    private router: Router,
    private toast: NgToastService
  ) {}

  ngOnInit(): void {
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      emailAddress: [null, [Validators.required, Validators.email]],
      password: [null, [Validators.required, Validators.minLength(5), Validators.maxLength(10)]],
      confirmPassword: [null, Validators.required]
    }, { validator: this.passwordCompareValidator });
  }

  passwordCompareValidator(fc: AbstractControl): ValidationErrors | null {
    return fc.get('password')?.value === fc.get('confirmPassword')?.value ? null : { notmatched: true };
  }

  get firstName() {
    return this.registerForm.get('firstName') as FormControl;
  }

  get lastName() {
    return this.registerForm.get('lastName') as FormControl;
  }

  get emailAddress() {
    return this.registerForm.get('emailAddress') as FormControl;
  }

  get password() {
    return this.registerForm.get('password') as FormControl;
  }

  get confirmPassword() {
    return this.registerForm.get('confirmPassword') as FormControl;
  }
  OnSubmit() {
    this.formValid = true;
    console.log("Form valid state before submit:", this.registerForm.valid);
    if (this.registerForm.valid) {
      const registerData = {
        firstName: this.registerForm.value.firstName,
        lastName: this.registerForm.value.lastName,
        emailAddress: this.registerForm.value.emailAddress,
        password: this.registerForm.value.password,
        confirmPassword: this.registerForm.value.confirmPassword,
        userType: 'user',
        phoneNumber: '1234567890'
      };
  
      console.log("Register data:", registerData);
  
      this.service.registerUser(registerData).subscribe(
        (response: any) => {
          console.log("Response:", response);
          this.toast.success({ detail: "SUCCESS", summary: response.message, duration: 3000 });
          setTimeout(() => {
            this.router.navigate(['admin/userPage']);
          }, 1000);
        },
        (error: any) => {
          console.log("Error:", error);
          // Display the error message from the server if available
          const errorMessage = error.message || error.error || 'Some other error occurred';
          this.toast.error({ detail: "ERROR", summary: errorMessage, duration: 3000 });
        }
      );
      this.formValid = false;
    } else {
      console.log("Form is invalid:", this.registerForm);
    }
  }
  
  // OnSubmit() {
  //   this.formValid = true;
  //   if (this.registerForm.valid) {
  //     const registerData = {
  //       firstName: this.registerForm.value.firstName,
  //       lastName: this.registerForm.value.lastName,
  //       emailAddress: this.registerForm.value.emailAddress,
  //       password: this.registerForm.value.password,
  //       confirmPassword: this.registerForm.value.confirmPassword,
  //       userType: 'user',
  //       phoneNumber: '1234567890'
  //     };

  //     console.log("Register data:", registerData);

  //     this.service.registerUser(registerData).subscribe(
  //       (response: any) => {
  //         console.log("Response:", response);
  //         this.toast.success({ detail: "SUCCESS", summary: response.message, duration: 3000 });
  //         setTimeout(() => {
  //           this.router.navigate(['admin/userPage']);
  //         }, 1000);
  //       },
  //       (error: any) => {
  //         console.log("Error:", error);
  //         this.toast.error({ detail: "ERROR", summary: error.message || error.error, duration: 3000 });
  //       }
  //     );
  //     this.formValid = false;
  //   } else {
  //     console.log("Form is invalid:", this.registerForm);
  //   }
  // }
}
