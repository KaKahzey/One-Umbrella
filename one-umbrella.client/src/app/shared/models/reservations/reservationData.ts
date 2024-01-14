export interface ReservationData {
    restaurantId : number,
    humanId : number |null,
    reservationTimeStart : Date,
    reservationTimeEnd : Date,
    tableId : number
}