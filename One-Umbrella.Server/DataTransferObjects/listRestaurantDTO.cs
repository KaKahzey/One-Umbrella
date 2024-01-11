namespace OneUmbrella.Server.DataTransferObjects
{
    public class ListRestaurantDTO
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantStreet { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantPostCode { get; set; }
        public string RestaurantDescription { get; set; }
        public int RestaurantRating { get; set; }
        public int RestaurantTotalRatings { get; set; }
        public string RestaurantImage { get; set; }
    }
}
