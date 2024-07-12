import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { AdminloginService } from 'src/app/service/adminlogin.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(
    public fb: FormBuilder,
    private http: HttpClient,
    public router: Router,
    public toastr: ToastrService,
    public toast: NgToastService,
    private service: AdminloginService
  ) { }

  loginForm: FormGroup;
  formValid: boolean;

  ngOnInit(): void {
    this.loginUser();
  }

  loginUser() {
    this.loginForm = this.fb.group({
      emailAddress: [null, Validators.compose([Validators.required, Validators.email])],
      password: [null, Validators.compose([Validators.required])]
    });
  }

  get emailAddress() {
    return this.loginForm.get('emailAddress') as FormControl;
  }

  get password() {
    return this.loginForm.get('password') as FormControl;
  }

  OnSubmit(){
    this.formValid = true;
    if(this.loginForm.valid)
    {
      this.service.loginUser([
        this.loginForm.value.emailAddress,
        this.loginForm.value.password
      ]).subscribe((res:any)=>{

        if(res)
        {
          console.log(res);
          if(res.message == "Login Successfully")
          {
            this.service.setToken(res.token);
            let tokenpayload = this.service.decodedToken();
            this.service.setCurrentUser(tokenpayload);

            this.toast.success({detail:"SUCCESS",summary:res.message,duration:3000});
            if(tokenpayload.userType == 'admin')
            {
              this.router.navigate(['admin/dashboard']);
            }
            else
            {
              this.router.navigate(['/home']);
            }

          }
          else
          {
            // this.toastr.error(res.data.message);
            this.toast.error({detail:"ERROR",summary:res.message,duration:3000});
          }
        }
        else
        {
          // this.toastr.error(res.message);
          this.toast.error({detail:"ERROR",summary:res.message,duration:3000});
        }
      });
      this.formValid = false;
    }
}
}