import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateMissionThemeComponent } from './update-mission-theme.component';

describe('UpdateMissionThemeComponent', () => {
  let component: UpdateMissionThemeComponent;
  let fixture: ComponentFixture<UpdateMissionThemeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateMissionThemeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateMissionThemeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
