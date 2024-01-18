import { Injectable } from '@angular/core';
import { ReservationUser } from '../models/reservations/reservationUser';
import { ApiService } from './api.service';
import { AuthService } from './auth.service';
import { Favorite } from '../models/navbar/favorite';

@Injectable({
  providedIn: 'root'
})
export class InfoService {

  
  reservations : ReservationUser[] = []
  favorites : Favorite[] = []

  constructor(private _apiService : ApiService, private _authService : AuthService){}

  setReservations() : void {
    this._apiService.getReservationsByUser(this._authService.getUser()!).subscribe({
      next : (resp) => {
        this.reservations = resp
        this.reservations.forEach(element => {
          element.reservationTimeStart = new Date(element.reservationTimeStart)
          element.reservationTimeEnd = new Date(element.reservationTimeEnd)
        })
      },
      error : error => console.error(error)   
      
    })
  }

  getReservations() : ReservationUser[] {
    return this.reservations
  }

  setFavorites() : void {
    this._apiService.getFavorites(this._authService.getUser()!).subscribe({
      next : resp => this.favorites = resp
      ,
      error : error => console.error(error)   
      
    })
  }

  getFavorites() : Favorite[] {
    return this.favorites
  }

  modifyFavorite(restaurantId : number) : boolean {
    const alreadyFavorite = this.getFavorites().some(r => r.restaurantId == restaurantId)
    if(alreadyFavorite){
     this._apiService.deleteFavorite(this._authService.getUser()!, restaurantId).subscribe({
      next : () => this.setFavorites(),
      error : error => console.error(error)
     })
    }
    else{
      this._apiService.createFavorite(this._authService.getUser()!, restaurantId).subscribe({
        next : () => this.setFavorites(),
        error : error => console.error(error)
      })
    }
    return alreadyFavorite
  }

  emptyArrays() : void {
    this.favorites = []
    this.reservations = []
  }
}
