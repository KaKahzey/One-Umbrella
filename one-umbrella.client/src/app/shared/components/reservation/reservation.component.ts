import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Reservation } from '../../models/reservations/reservation';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {
  reservations : Reservation[] = [
    {reservationId : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", reservationTimeStart : new Date, reservationTimeEnd : new Date, reservationStatus : 0},
    {reservationId : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", reservationTimeStart : new Date, reservationTimeEnd : new Date, reservationStatus : 1},
    {reservationId : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", reservationTimeStart : new Date, reservationTimeEnd : new Date, reservationStatus : 2},
    {reservationId : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", reservationTimeStart : new Date, reservationTimeEnd : new Date, reservationStatus : 2},
    {reservationId : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", reservationTimeStart : new Date, reservationTimeEnd : new Date, reservationStatus : 2},
    {reservationId : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", reservationTimeStart : new Date, reservationTimeEnd : new Date, reservationStatus : 2}
  ]

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
  
    return months[month];
  }

  addZero(value: number): string {
    return value < 10 ? `0${value}` : `${value}`;
  }


}
