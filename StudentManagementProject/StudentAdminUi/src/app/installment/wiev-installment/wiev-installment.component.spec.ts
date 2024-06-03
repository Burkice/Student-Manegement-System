import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WievInstallmentComponent } from './wiev-installment.component';

describe('WievInstallmentComponent', () => {
  let component: WievInstallmentComponent;
  let fixture: ComponentFixture<WievInstallmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [WievInstallmentComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(WievInstallmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
