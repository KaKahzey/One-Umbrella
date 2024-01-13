import { onlyBlob } from "../shared/onlyBlob";

export interface Restaurant {
    restaurantId : number,
    ownerId : number,
    restaurantName : string,
    restaurantStreet : string,
    restaurantCity : string,
    restaurantPostCode : string,
    restaurantRating : number,
    restaurantDescription : string,
    images : onlyBlob[]
}