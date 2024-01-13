export interface Table {
    tableId : number,
    gridId : number,
    rowIndex : number,
    columnIndex : number,
    endRowIndex : number,
    endColumnIndex : number,
    seatCapability : string,
    tableType : number
    //1 square, 2 rectangle, 3 round
}