import { Component } from '@angular/core';
import { ApiService } from '../../../shared/services/api.service';
import { ListRestaurant } from '../../../shared/models/list-restaurant/listRestaurant';
import { AuthService } from '../../../shared/services/auth.service';
import { RouterLink } from '@angular/router';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { RestaurantData } from '../../../shared/models/restaurant/restaurantData';
import { RatingModule } from 'primeng/rating';

@Component({
  selector: 'app-own-restaurants',
  standalone: true,
  imports: [RouterLink, DialogModule, ButtonModule, FormsModule, ReactiveFormsModule, InputTextareaModule, RatingModule],
  templateUrl: './own-restaurants.component.html',
  styleUrl: './own-restaurants.component.scss'
})
export class OwnRestaurantsComponent {

  restaurants : ListRestaurant[] = []
  userId : number | null = null
  visible : boolean = false
  loading: boolean = false

  createForm : FormGroup

  constructor(private _apiService : ApiService, private _authService : AuthService,  private _fb : FormBuilder) {
    this.createForm = this._fb.group({
      restaurantName :[null, [Validators.required]],
      restaurantStreet :[null, [Validators.required]],
      restaurantCity :[null, [Validators.required]],
      restaurantPostCode :[null, [Validators.required]],
      restaurantDescription :[null, [Validators.required]]
    })
  }

  ngOnInit() : void {
    this.userId = this._authService.getUser()
    if(this.userId){
      this._apiService.getAllForOneOwner(this.userId).subscribe({
        next : (resp) => {
          this.restaurants = resp
        },
        error : (error) => {
          console.log(error)
        }
      })
    }
  }

  createRestaurant() : void {
    if(this.createForm.valid && this.userId){
      const newRestaurant : RestaurantData = {
        ownerId : this.userId,
        restaurantName : this.createForm.get("restaurantName")?.value,
        restaurantStreet : this.createForm.get("restaurantStreet")?.value,
        restaurantCity : this.createForm.get("restaurantCity")?.value,
        restaurantPostCode : this.createForm.get("restaurantPostCode")?.value,
        restaurantDescription : this.createForm.get("restaurantDescription")?.value
      }
      this._apiService.createRestaurant(newRestaurant).subscribe({
        next : (resp) => {
          console.log(resp)
          this.visible = false
          this.ngOnInit()
        },
        error : (error) => console.log(error)
      })
    }
  }

  setStars(score : number) : void {

  }

  showDialog() :void {
    this.visible = true;
  }

  load() {
    this.loading = true;

    setTimeout(() => {
        this.loading = false
    }, 2000);
  }
}
