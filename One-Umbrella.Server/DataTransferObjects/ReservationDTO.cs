﻿namespace OneUmbrella.Server.DataTransferObjects
{
    public class ReservationDTO
    {
        public int ReservationId { get; set; }
        public int RestaurantId { get; set; }
        public int HumanId { get; set; }
        public DateTime ReservationTimeStart { get; set; }
        public DateTime ReservationTimeEnd { get; set; }
        public int ReservationStatus { get; set; }
    }

    public class ReservationDataDTO
    {
        public int RestaurantId { get; set; }
        public int HumanId { get; set; }
        public DateTime ReservationTimeStart { get; set; }
        public DateTime ReservationTimeEnd { get; set; }
    }
}