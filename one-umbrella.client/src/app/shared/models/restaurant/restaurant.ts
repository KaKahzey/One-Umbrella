import { onlyBlob } from "../shared/onlyBlob";

export interface Restaurant {
    id : number,
    name : string,
    street : string,
    city : string,
    postCode : string,
    rating : number,
    description : string,
    images : onlyBlob[]
}