import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import * as $ from 'jquery';
import { AdminloginService } from 'src/app/service/adminlogin.service';
import { ClientService } from 'src/app/service/client.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private loginService:AdminloginService,private router:Router, public service: ClientService,) { }
  isLogin:boolean = false;
  userDetail:any;
  loginUserId:any;
  userImage:any;
  userImages:any;
  activeLink: string = '';
  ngOnInit(): void {
    this.loginService.getCurrentUser().subscribe((data:any)=>{
      let userName = this.loginService.getUserDetail();
      if(userName != null || data != null){
        console.log(userName)
        this.isLogin = true;
        data == null ? (this.userDetail = userName.fullName) : (this.userDetail = data.fullName);
        data == null ? (this.loginUserId = userName.userId) : (this.loginUserId = data.userId);
        const userId = data == null ? userName.userId : data.userId;
        this.service.GetUserProfileDetailById(userId).subscribe((userDetails: any) => {
          if (userDetails && userDetails.userDetails && userDetails.userDetails.userImage) {
            this.userImage = userDetails.userDetails.userImage;
          } else {
            this.userImage = 'assets/NoUser.png'; // Fallback image
          }
        });
      }
    });
    var tokenDetail = this.loginService.decodedToken();
    console.log(tokenDetail)
    if(tokenDetail.userType != 'user')
    {
      this.isLogin = false;
    }
  }
  

  setActiveLink(url: string) {
    if (url.includes('home')) {
      this.activeLink = 'home';
    } else if (url.includes('policy')) {
      this.activeLink = 'policy';
    } else {
      this.activeLink = 'support';
    }
  }
  RedirectLogin(){
    this.router.navigate(['']);
  }
  RedirectRegister(){
    this.router.navigate(['register']);
  }
  LoggedOut(){
    this.loginService.LoggedOut();
    this.isLogin = false;
    this.router.navigate(['']);
}
}

