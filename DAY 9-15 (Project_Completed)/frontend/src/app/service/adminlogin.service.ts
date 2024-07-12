import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { user } from '../model/user.model';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AdminloginService {
  baseUrl: string = 'https://localhost:7178/api/v1';
  apiUrl : string = 'https://localhost:7178/api/v1/Auth';
  imageUrl: string = 'http://localhost:56577';
  currentUser: BehaviorSubject<any> = new BehaviorSubject(null);
  currentUserName: BehaviorSubject<any> = new BehaviorSubject(null);
  currentUserData: any;
  jwtHelperService = new JwtHelperService();

  constructor(public http: HttpClient) {}

  registerUser(registerData: any): Observable<any> {
    const url = `${this.baseUrl}/UserDetails/registerUser`;
    return this.http.post(url, registerData);
  }

  GetUserById(id: number): Observable<user[]> {
    console.log("Id received in getuser function is : ", id);
    return this.http.get<user[]>(`${this.baseUrl}/UserDetails/getUserById/${id}`);
  }

  UpdateUser(data: user) {
    return this.http.put(`${this.baseUrl}/UserDetails/updateUser`, data);
  }

  loginUser(loginInfo: Array<string>) {
    return this.http.post(`${this.apiUrl}/login`, { EmailAddress: loginInfo[0], Password: loginInfo[1] }, { responseType: 'json' });
  }

  ForgotPasswordEmailCheck(data: any) {
    return this.http.post(`${this.apiUrl}/ForgotPassword`, data);
  }

  ResetPassword(data: any) {
    return this.http.post(`${this.apiUrl}/ResetPassword`, data, { responseType: 'text' });
  }

  ChangePassword(data: any) {
    return this.http.post(`${this.apiUrl}/ChangePassword`, data);
  }

  getToken() {
    return localStorage.getItem("access_Token");
  }

  setToken(token: string) {
    localStorage.setItem("access_Token", token);
  }

  isLoggedIn(): boolean {
    return localStorage.getItem("access_Token") ? true : false;
  }

  LoggedOut() {
    localStorage.removeItem("access_Token");
  }

  public getCurrentUser() {
    return this.currentUser.asObservable();
  }

  public setCurrentUser(userDetail: any) {
    return this.currentUser.next(userDetail);
  }

  decodedToken(): any {
    const token = this.getToken();
    if (token) {
      return this.jwtHelperService.decodeToken(token);
    }
    return null;
  }

  getUserFullName() {
    const decodedToken = this.decodedToken();
    if (decodedToken) {
      return decodedToken.fullName;
    }
    return null;
  }

  getUserDetail() {
    return this.decodedToken();
  }
}