import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateMissionSkillComponent } from './update-mission-skill.component';

describe('UpdateMissionSkillComponent', () => {
  let component: UpdateMissionSkillComponent;
  let fixture: ComponentFixture<UpdateMissionSkillComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateMissionSkillComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateMissionSkillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
