/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HekimPagesComponent } from './hekimPages.component';

describe('HekimPagesComponent', () => {
  let component: HekimPagesComponent;
  let fixture: ComponentFixture<HekimPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HekimPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HekimPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
