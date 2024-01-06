import { Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ListRestaurantsComponent } from './pages/customer/list-restaurants/list-restaurants.component';

export const routes: Routes = [
  { path : "", component: ListRestaurantsComponent },
  { path : "**", component : NotFoundComponent }
];
