import { Component } from '@angular/core';
import { ListRestaurant } from '../../../shared/models/list-restaurant/listRestaurant';
import { ApiService } from '../../../shared/services/api.service';
import { RouterLink } from '@angular/router';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-list-restaurants',
  standalone: true,
  imports: [RouterLink, RatingModule, FormsModule],
  templateUrl: './list-restaurants.component.html',
  styleUrl: './list-restaurants.component.scss'
})
export class ListRestaurantsComponent {
  restaurants : ListRestaurant[] = []
  page : number = 1
  pageSize : number = 10
  pageIndex : number = 1

  constructor(private _apiService : ApiService) {
    
  }

  ngOnInit() : void {
    this._apiService.getAllRestaurantsByPagination(1, 5, "restaurant_name", false).subscribe({
      next : (resp) => {
        this.restaurants = resp
        this.restaurants.forEach(restaurant => {          
          restaurant.restaurantRating = Math.floor(restaurant.restaurantRating)
        this._apiService.getFrontImage(restaurant.restaurantId).subscribe({
          next : resp => {
            resp.imageData? restaurant.restaurantImage = resp : restaurant.restaurantImage = ""
          },
          error : error => console.error(error)
          
        })          
        })
      },
      error : (error) => {
        console.log(error)
      }
    })
  }

  getList(page : number, pageSize : number, sortBy : string, isDescending : boolean) : void {

  }
  setStars(score : number) : void {

  }

}
