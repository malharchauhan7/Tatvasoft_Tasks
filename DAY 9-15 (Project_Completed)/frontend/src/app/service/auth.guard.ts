import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { AdminloginService } from './adminlogin.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private adminloginService: AdminloginService,
    private router: Router,
    private toastr: NgToastService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    if (this.adminloginService.isLoggedIn()) {
      return true;
    } else {
      this.toastr.error({
        detail: "ERROR",
        summary: 'Invalid Client Request',
        duration: 3000
      });
      this.router.navigate(['/login']);
      return false;
    }
  }
}