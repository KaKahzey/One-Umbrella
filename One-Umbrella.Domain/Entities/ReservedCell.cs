using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class ReservedCell
    {
        public int ReservationId { get; set; }
        public int GridId { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
    }
}
