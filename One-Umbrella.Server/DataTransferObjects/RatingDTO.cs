namespace OneUmbrella.Server.DataTransferObjects
{
    public class RatingDTO
    {
        public int HumanId { get; set; }
        public int RestaurantId { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }

    public class RatingDataDTO
    {
        public int HumanId { get; set; }
        public int RestaurantId { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
