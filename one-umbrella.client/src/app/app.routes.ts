import { Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ListRestaurantsComponent } from './pages/customer/list-restaurants/list-restaurants.component';
import { RestaurantComponent } from './pages/customer/restaurant/restaurant.component';
import { ModifyRestaurantComponent } from './pages/owner/modify-restaurant/modify-restaurant.component';
import { OwnRestaurantsComponent } from './pages/owner/own-restaurants/own-restaurants.component';

export const routes: Routes = [
  { path : "", component: ListRestaurantsComponent },
  { path : "restaurant/:id", component : RestaurantComponent },
  { path : "mes-restaurants", component : OwnRestaurantsComponent },
  { path : "modification", component : ModifyRestaurantComponent },
  { path : "**", component : NotFoundComponent }
];
