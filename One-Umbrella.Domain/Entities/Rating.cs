using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class Rating
    {
        public int HumanId { get; set; }
        public int RestaurantId { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
    }
}
