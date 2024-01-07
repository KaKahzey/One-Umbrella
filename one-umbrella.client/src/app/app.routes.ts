import { Routes } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ListRestaurantsComponent } from './pages/customer/list-restaurants/list-restaurants.component';
import { RestaurantComponent } from './pages/customer/restaurant/restaurant.component';
import { CreateRestaurantComponent } from './pages/owner/create-restaurant/create-restaurant.component';
import { ModifyRestaurantComponent } from './pages/owner/modify-restaurant/modify-restaurant.component';

export const routes: Routes = [
  { path : "", component: ListRestaurantsComponent },
  { path : "restaurant/:id", component : RestaurantComponent },
  { path : "création", component : CreateRestaurantComponent },
  { path : "modification", component : ModifyRestaurantComponent },
  { path : "**", component : NotFoundComponent }
];
