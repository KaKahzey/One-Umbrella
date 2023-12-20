import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OwnRestaurantsComponent } from './own-restaurants.component';

describe('OwnRestaurantsComponent', () => {
  let component: OwnRestaurantsComponent;
  let fixture: ComponentFixture<OwnRestaurantsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [OwnRestaurantsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(OwnRestaurantsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
