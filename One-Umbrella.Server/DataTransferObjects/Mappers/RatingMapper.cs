using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class RatingMapper
    {
        public static RatingDTO ToDTO(this Rating rating)
        {
            return new RatingDTO()
            {
                HumanId = rating.HumanId,
                RestaurantId = rating.RestaurantId,
                Score = rating.Score,
                Comment = rating.Comment
            };
        }
        public static Rating ToEntity(this RatingDataDTO rating)
        {
            return new Rating()
            {
                HumanId = rating.HumanId,
                RestaurantId = rating.RestaurantId,
                Score = rating.Score,
                Comment = rating.Comment
            };
        }
    }
}
