export interface Reservation {
    id : number,
    restaurantId : number,
    restaurantName : string,
    humanId : number,
    timeStart : Date,
    timeEnd : Date,
    status : number
    //1 for pending, 0 for denied, 2 for validated, 3 for denied-seen
}