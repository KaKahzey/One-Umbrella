import { StructuralElementData } from "./structuralElementData";
import { TableData } from "./tableData";

export interface GridData {
    restaurantId : number,
    gridName : string,
    gridRows : number,
    gridColumns : number,
    gridTables : TableData[],
    gridElements : StructuralElementData[]
}