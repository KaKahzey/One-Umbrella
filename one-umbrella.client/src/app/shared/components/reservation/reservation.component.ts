import { Component } from '@angular/core';
import { Reservation } from '../../models/reservations/reservation';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {
  reservations : Reservation[] = [
    {id : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", humanId : 1, timeStart : new Date, timeEnd : new Date, status : 0},
    {id : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", humanId : 1, timeStart : new Date, timeEnd : new Date, status : 1},
    {id : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", humanId : 1, timeStart : new Date, timeEnd : new Date, status : 2},
    {id : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", humanId : 1, timeStart : new Date, timeEnd : new Date, status : 2},
    {id : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", humanId : 1, timeStart : new Date, timeEnd : new Date, status : 2},
    {id : 1, restaurantId : 1, restaurantName : "testzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz", humanId : 1, timeStart : new Date, timeEnd : new Date, status : 2}
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
