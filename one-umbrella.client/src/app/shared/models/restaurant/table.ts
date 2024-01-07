export interface Table {
    id : number,
    rowIndex : number,
    columnIndex : number,
    endRowIndex : number,
    endColumnIndex : number,
    seatCapability : string,
    type : number
    //1 square, 2 rectangle, 3 round
}