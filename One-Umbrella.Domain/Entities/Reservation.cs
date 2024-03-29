﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int RestaurantId { get; set; }
        public int HumanId { get; set; }
        public DateTime ReservationTimeStart { get; set; }
        public DateTime ReservationTimeEnd { get; set; }
        public int ReservationStatus { get; set; }
        // 0 for pending, 1 for validated, 2 for denied,  3 for denied-seen
    }
}
