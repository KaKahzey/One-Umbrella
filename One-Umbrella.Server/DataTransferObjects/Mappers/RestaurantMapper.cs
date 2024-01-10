using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class RestaurantMapper
    {
        public static RestaurantDTO ToDTO(this Restaurant restaurant, IEnumerable<ImageRestaurantDTO> images)
        {
            return new RestaurantDTO()
            {
                RestaurantId = restaurant.RestaurantId,
                OwnerId = restaurant.HumanId,
                RestaurantName = restaurant.RestaurantName,
                RestaurantStreet = restaurant.RestaurantStreet,
                RestaurantCity = restaurant.RestaurantCity,
                RestaurantPostCode = restaurant.RestaurantPostCode,
                RestaurantDescription = restaurant.RestaurantDescription,
                RestaurantImages = images
            };
        }
        public static Restaurant ToEntity(this RestaurantDataDTO restaurant)
        {
            return new Restaurant()
            {
                RestaurantName = restaurant.RestaurantName,
                HumanId = restaurant.OwnerId,
                RestaurantStreet = restaurant.RestaurantStreet,
                RestaurantCity = restaurant.RestaurantCity,
                RestaurantPostCode = restaurant.RestaurantPostCode,
                RestaurantDescription = restaurant.RestaurantDescription
            };
        }
    }
}
