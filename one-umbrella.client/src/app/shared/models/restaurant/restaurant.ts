import { onlyBlob } from "../shared/onlyBlob";

export interface Restaurant {
    RestaurantId : number,
    OwnerId : number,
    RestaurantName : string,
    RestaurantStreet : string,
    RestaurantCity : string,
    RestaurantPostCode : string,
    RestaurantRating : number,
    RestaurantDescription : string,
    images : onlyBlob[]
}