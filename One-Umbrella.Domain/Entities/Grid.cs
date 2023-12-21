using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One_Umbrella.Domain.Entities
{
    internal class Grid
    {
        public int GridId { get; set; }
        public string GridName { get; set; }
        public int RestaurantId { get; set; }
    }
}
