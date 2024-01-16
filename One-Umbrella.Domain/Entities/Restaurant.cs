using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class Restaurant
    {
        public int RestaurantId { get; set; }
        public int HumanId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantStreet { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantPostCode { get; set; }
        public string RestaurantDescription { get; set; }
        public decimal RestaurantRating { get; set; }
    }
}
