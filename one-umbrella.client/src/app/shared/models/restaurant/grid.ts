import { StructuralElment } from "./structuralElement";
import { Table } from "./table";

export interface Grid {
    id : number,
    restaurantId : number,
    name : string,
    rows : number,
    columns : number,
    tables : Table[],
    elements : StructuralElment[]
}