using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class Grid
    {
        public int GridId { get; set; }
        public string? GridName { get; set; }
        public int RestaurantId { get; set; }
        public int GridRows { get; set; }
        public int GridColumns { get; set; }
    }
}
