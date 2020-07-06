import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AuctionAddEditComponent } from './auction-add-edit.component';

describe('AuctionAddEditComponent', () => {
  let component: AuctionAddEditComponent;
  let fixture: ComponentFixture<AuctionAddEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AuctionAddEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuctionAddEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
