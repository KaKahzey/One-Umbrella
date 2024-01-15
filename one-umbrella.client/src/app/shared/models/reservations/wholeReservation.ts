import { Reservation } from "./reservation";
import { ReservedTable } from "./reservedTable";

export interface WholeReservation {
    reservations : Reservation[],
    reservedTables : ReservedTable[]
}