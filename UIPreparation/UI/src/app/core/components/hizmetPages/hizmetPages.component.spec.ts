/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { HizmetPagesComponent } from './hizmetPages.component';

describe('HizmetPagesComponent', () => {
  let component: HizmetPagesComponent;
  let fixture: ComponentFixture<HizmetPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HizmetPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HizmetPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
