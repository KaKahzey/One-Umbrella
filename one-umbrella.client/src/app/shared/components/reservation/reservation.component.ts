import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ReservationUser } from '../../models/reservations/reservationUser';
import { ApiService } from '../../services/api.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {
  reservations : ReservationUser[] = []

  constructor(private _apiService : ApiService, private _authService : AuthService){}

  ngOnInit() : void {
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

  setReservationStatus(status : number) : string{
    switch(status){
      case 0:
        return "pending"
      case 1:
        return "validated"
      case 2:
        return "denied"
      default:
        return "error"
    }
  }

  getMonthName(month: number): string {
    const months = [
      'Janvier', 'Février', 'Mars', 'Avril',
      'Mai', 'Juin', 'Juillet', 'Août',
      'Septembre', 'Octobre', 'Novembre', 'Décembre'
    ];
  
    return months[month]
  }

  addZero(value: number): string {
    return value < 10 ? `0${value}` : `${value}`
  }

  updateStatus(id : number) : void {
    this._apiService.setReservationStatus(id, 3).subscribe({
      next : resp => this.ngOnInit(),
      error : error => console.error(error)
      
    })
  }

}
