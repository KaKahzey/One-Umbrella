import { StructuralElement } from "./structuralElement";
import { Table } from "./table";

export interface Grid {
    gridId : number,
    restaurantId : number,
    gridName : string,
    gridRows : number,
    gridColumns : number,
    gridTables : Table[],
    gridElements : StructuralElement[]
}