﻿namespace OneUmbrella.Server.DataTransferObjects
{
    public class ImageRestaurantDTO
    {
        public int ImageId { get; set; }
        public int RestaurantId { get; set; }
        public string ImageData { get; set; }
        public bool IsFront { get; set; }
        public bool IsMenu { get; set; }
    }

    public class ImageRestaurantDataDTO
    {
        public int RestaurantId { get; set; }
        public byte[] ImageData { get; set; }
        public bool IsFront { get; set; }
        public bool IsMenu { get; set; }
    }
}
