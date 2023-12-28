using System;
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
    }
}
