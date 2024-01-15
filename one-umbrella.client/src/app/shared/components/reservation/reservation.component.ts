import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ReservationUser } from '../../models/reservations/reservationUser';

@Component({
  selector: 'app-reservation',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './reservation.component.html',
  styleUrl: './reservation.component.scss'
})
export class ReservationComponent {
  reservations : ReservationUser[] = []

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
