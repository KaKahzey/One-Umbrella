using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class ListRestaurantMapper
    {
        public static ListRestaurantDTO ToDTO(Restaurant restaurant, int totalRatings, string image)
        {
            return new ListRestaurantDTO()
            {
                RestaurantId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName,
                RestaurantStreet = restaurant.RestaurantStreet,
                RestaurantCity = restaurant.RestaurantCity,
                RestaurantPostCode = restaurant.RestaurantPostCode,
                RestaurantDescription = restaurant.RestaurantDescription,
                RestaurantRating = restaurant.RestaurantRating,
                RestaurantTotalRatings = totalRatings,
                RestaurantImage = image ?? string.Empty
            };
        }
    }
}
