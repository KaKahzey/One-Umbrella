import { Component } from '@angular/core';
import { ListRestaurant } from '../../../shared/models/list-restaurant/listRestaurant';

@Component({
  selector: 'app-list-restaurants',
  standalone: true,
  imports: [],
  templateUrl: './list-restaurants.component.html',
  styleUrl: './list-restaurants.component.scss'
})
export class ListRestaurantsComponent {
  restaurants : ListRestaurant[] = [
    {
      id : 1,
      name : "McDonald",
      street : "Rue de la Fontaine",
      city : "Marbais",
      postCode : "1495",
      description : "Parfait domaine pour un moment en famille",
      rating : 4,
      image : ""
    },
    {
      id : 1,
      name : "McDonald",
      street : "Rue de la Fontaine",
      city : "Marbais",
      postCode : "1495",
      description : "Parfait domaine pour un moment en famille",
      rating : 4,
      image : ""
    }
  ]


  


}
