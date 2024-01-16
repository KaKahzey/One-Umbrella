using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects
{
    public class RestaurantDTO
    {
        public int RestaurantId { get; set; }
        public int OwnerId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantStreet { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantPostCode { get; set; }
        public string RestaurantDescription { get; set; }
        public decimal RestaurantRating { get; set; }
        public IEnumerable<ImageRestaurantDTO> RestaurantImages { get; set; }
    }
    public class RestaurantDataDTO
    {
        public int OwnerId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantStreet { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantPostCode { get; set; }
        public string RestaurantDescription { get; set; }
    }
}
