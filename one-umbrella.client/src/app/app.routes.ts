import { Routes } from '@angular/router';
import { HomeComponent } from './shared/components/home/home.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';

export const routes: Routes = [
  { path : "", component: HomeComponent },
  { path : "**", component : NotFoundComponent }
];