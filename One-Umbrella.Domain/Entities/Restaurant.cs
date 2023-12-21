using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_Umbrella.Domain.Entities
{
    internal class Restaurant
    {
        public int RestaurantId { get; set; }
        public int OwnerId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantStreet { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantPostCode { get; set; }
    }
}
