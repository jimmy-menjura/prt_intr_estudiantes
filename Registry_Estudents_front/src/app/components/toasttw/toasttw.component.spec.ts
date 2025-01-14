import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToasttwComponent } from './toasttw.component';

describe('ToasttwComponent', () => {
  let component: ToasttwComponent;
  let fixture: ComponentFixture<ToasttwComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ToasttwComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ToasttwComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
