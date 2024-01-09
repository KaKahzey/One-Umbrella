using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class FavoriteMapper
    {
        public static FavoriteDTO ToDTO(Restaurant restaurant)
        {
            return new FavoriteDTO()
            {
                HumanId = restaurant.RestaurantId,
                RestaurantName = restaurant.RestaurantName
            };
        }

        public static Favorite ToEntity(FavoriteDTO favorite, int id)
        {
            return new Favorite()
            {
                HumanId = favorite.HumanId,
                RestaurantId = id
            };
        }
    }
}
