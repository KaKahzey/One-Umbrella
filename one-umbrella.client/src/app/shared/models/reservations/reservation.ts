export interface Reservation {
    reservationId : number,
    restaurantId : number,
    humanId : number,
    reservationTimeStart : Date,
    reservationTimeEnd : Date,
    reservationStatus : number
    // 0 for pending, 1 for validated, 2 for denied,  3 for denied-seen
}