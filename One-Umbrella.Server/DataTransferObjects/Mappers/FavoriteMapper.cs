using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class FavoriteMapper
    {
        public static FavoriteDTO ToDTO(this Restaurant restaurant, int id)
        {
            return new FavoriteDTO()
            {
                HumanId = id,
                RestaurantId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName
            };
        }

        public static Favorite ToEntity(int humanId, int restaurantId)
        {
            return new Favorite()
            {
                HumanId = humanId,
                RestaurantId = restaurantId
            };
        }
    }
}
