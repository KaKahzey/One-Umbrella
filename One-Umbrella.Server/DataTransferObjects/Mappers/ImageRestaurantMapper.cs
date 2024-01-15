using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class ImageRestaurantMapper
    {
        public static ImageRestaurantDTO ToDTO(this ImageRestaurant image)
        {
            return new ImageRestaurantDTO()
            {
                ImageId = image.ImageId,
                RestaurantId = image.RestaurantId,
                ImageData = Convert.ToBase64String(image.ImageData),
                IsFront = image.IsFront,
                IsMenu = image.IsMenu
            };
        }

        public static ImageRestaurant ToEntity(this ImageRestaurantDataDTO image)
        {
            return new ImageRestaurant()
            {
                RestaurantId = image.RestaurantId,
                ImageData = image.ImageData,
                IsFront = image.IsFront,
                IsMenu = image.IsMenu
            };
        }

    }
}
