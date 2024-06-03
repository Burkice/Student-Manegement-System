import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WievPaymentComponent } from './wiev-payment.component';

describe('WievPaymentComponent', () => {
  let component: WievPaymentComponent;
  let fixture: ComponentFixture<WievPaymentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WievPaymentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WievPaymentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
