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
                RestaurantName = restaurant.RestaurantName,
                RestaurantStreet = restaurant.RestaurantStreet,
                RestaurantCity = restaurant.RestaurantCity,
                RestaurantPostCode = restaurant.RestaurantPostCode,
                RestaurantDescription = restaurant.RestaurantDescription,
                RestaurantRating = restaurant.RestaurantRating,
                RestaurantImages = images
            };
        }
    }
}
