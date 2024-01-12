import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive, RouterModule, RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
import { ReservationComponent } from './shared/components/reservation/reservation.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, FormsModule, ReactiveFormsModule, HttpClientModule, RouterModule, NavbarComponent, ReservationComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: []

})
export class AppComponent {
  title = 'One-Umbrella';

  constructor() {}

}
