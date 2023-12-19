# OneUmbrella

restaurant :
---
    restaurantId : number,
    name : string,
    address : {
        street : string,
        city : string,
        postCode : string
    },
    tablesCount : number,
    disposal : string[],
    averageMealTime : number
---

tables : 
---
    tableId : number,
    sizeMin : number,
    sizeMax : number,
    <!-- 1 : available, 2 : pending, 3 : taken -->
    reserved : number
---

reservation :
---
    reservationId : number,
    customerName : string,
    numberOfPeople : number,
    tablesUsed : [],
    status : string,
---
