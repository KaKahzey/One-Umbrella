import { Component } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { Router, RouterLink, RouterLinkActive, RouterModule, RouterOutlet } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NavbarComponent } from './shared/components/navbar/navbar.component';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, RouterLinkActive, FormsModule, ReactiveFormsModule, HttpClientModule, RouterModule, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  providers: []

})
export class AppComponent {
  title = 'One-Umbrella';




}
