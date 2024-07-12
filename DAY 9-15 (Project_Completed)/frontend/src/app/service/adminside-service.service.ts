import { Injectable } from '@angular/core';
import { HttpClient, HttpHandler, HttpHeaders } from '@angular/common/http';
import { City, CMS, Country, Mission,MissionDto } from '../model/cms.model';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { MissionApplication } from '../model/missionApplication.model';
import { MissionTheme } from '../model/missionTheme.model';
import { MissionSkill } from '../model/missionSkill.model';
import { UserDetail } from '../model/user.model'; 

@Injectable({
  providedIn: 'root',
})

export class AdminsideServiceService {
  constructor(
    public http: HttpClient,
    public toastr: ToastrService,
    public router: Router,
   
  ) {}
  // apiUrl:string='http://localhost:63943/api';
  apiUrl: string = 'https://localhost:7178/api';
  imageUrl: string = 'http://localhost:56577';


  
  // User Details
  getUserDetails(): Observable<UserDetail[]> {
    return this.http.get<UserDetail[]>(`${this.apiUrl}/v1/UserDetails`);
  }

  getUserDetailById(id: number): Observable<UserDetail> {
    return this.http.get<UserDetail>(`${this.apiUrl}/v1/UserDetails/${id}`);
  }

  createUserDetail(userDetail: UserDetail): Observable<UserDetail> {
    return this.http.post<UserDetail>(`${this.apiUrl}/v1/UserDetails`, userDetail);
  }

  updateUserDetail(userDetail: UserDetail): Observable<UserDetail> {
    return this.http.put<UserDetail>(`${this.apiUrl}/v1/UserDetails/${userDetail.id}`, userDetail);
  }

  deleteUserDetail(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/v1/UserDetails/${id}`);
  }

  //CMS
  CMSList(): Observable<CMS[]> {
    return this.http.get<CMS[]>(`${this.apiUrl}/CMS/CMSList`);
  }
  CMSDetailById(id: number): Observable<CMS[]> {
    return this.http.get<CMS[]>(`${this.apiUrl}/CMS/CMSDetailById/${id}`);
  }
  AddCMS(data: CMS) {
    return this.http.post(`${this.apiUrl}/CMS/AddCMS`, data, {
      responseType: 'json',
    });
  }
  UpdateCMS(data: CMS) {
    return this.http.post(`${this.apiUrl}/CMS/UpdateCMS`, data);
  }
  DeleteCMS(data: any) {
    return this.http.delete(`${this.apiUrl}/CMS/DeleteCMS/${data}`);
  }

  //Mission
  GetMissionThemeList():Observable<MissionTheme[]>{
    return this.http.get<MissionTheme[]>(`${this.apiUrl}/MissionTheme`);
  }
  GetMissionSkillList():Observable<MissionSkill[]>{
    return this.http.get<MissionSkill[]>(`${this.apiUrl}/MissionSkill`);
  }
  UploadImage(data: any) {
    return this.http.post(`${this.apiUrl}/Common/UploadImage`,data);
  }
  UploadDoc(data: any) {
    return this.http.post(`${this.apiUrl}/Mission/UploadImage`,data);
  }
  MissionList(): Observable<Mission[]> {
    return this.http.get<Mission[]>(`${this.apiUrl}/Missions`);
  }
  MissionDetailById(id: number): Observable<Mission[]> {
    return this.http.get<Mission[]>(
      `${this.apiUrl}/Mission/${id}`
    );
  }
  CountryList(): Observable<Country[]> {
    return this.http.get<Country[]>(`${this.apiUrl}/Countries`);
  }
  CityList(countryId: any): Observable<City[]> {
    return this.http.get<City[]>(`${this.apiUrl}/Cities/GetCitiesByCountryId/${countryId}`);
  }
  AddMission(missionDto: MissionDto) {
    return this.http.post<Mission>(`${this.apiUrl}/Missions`, missionDto);
  }
  
  UpdateMission(data: Mission) {
    return this.http.post(`${this.apiUrl}/Missions/${data.id}`, data);
  }
  DeleteMission(data: any) {
    return this.http.delete(`${this.apiUrl}/Missions/${data}`);
  }

  //Mission Application
  MissionApplicationList(): Observable<MissionApplication[]> {
    return this.http.get<MissionApplication[]>(
      `${this.apiUrl}/MissionApplication`
    );
  }

  MissionApplicationDelete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/MissionApplication/${id}`);
  }

  MissionApplicationApprove(id: number): Observable<any> {
    return this.http.put(`${this.apiUrl}/MissionApplication/Approve/${id}`, {});
  }

  //Mission Theme
  MissionThemeList(): Observable<MissionTheme[]> {
    return this.http.get<MissionTheme[]>(
      `${this.apiUrl}/MissionTheme`
    );
  }
  MissionThemeById(id: any): Observable<MissionTheme[]> {
    return this.http.get<MissionTheme[]>(
      `${this.apiUrl}/MissionTheme/${id}`
    );
  }
  AddMissionTheme(data: MissionTheme) {
    return this.http.post(`${this.apiUrl}/MissionTheme/`, data);
  }
  UpdateMissionTheme(data: MissionTheme) {
    return this.http.put(
      `${this.apiUrl}/MissionTheme/${data.id}`,
      data
    );
  }
  DeleteMissionTheme(data: any) {
    return this.http.delete(
      `${this.apiUrl}/MissionTheme/${data}`
    );
  }

  //Mission Skill
  MissionSkillList(): Observable<MissionSkill[]> {
    return this.http.get<MissionSkill[]>(
      `${this.apiUrl}/MissionSkill`
    );
  }
  MissionSkillById(id: any): Observable<MissionSkill[]> {
    return this.http.get<MissionSkill[]>(
      `${this.apiUrl}/MissionSkill/${id}`
    );
  }
  AddMissionSkill(data: MissionSkill) {
    return this.http.post(`${this.apiUrl}/MissionSkill`, data);
  }
  UpdateMissionSkill(data: MissionSkill) {
    return this.http.put(
      `${this.apiUrl}/MissionSkill/${data.id}`,
      data
    );
  }
  DeleteMissionSkill(data: any) {
    return this.http.delete(
      `${this.apiUrl}/MissionSkill/${data}`
    );
  }
}
