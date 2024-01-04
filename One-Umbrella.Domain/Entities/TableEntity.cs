using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class TableEntity
    {
        public int TableId { get; set; }
        public int GridId { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public int EndRowIndex { get; set; }
        public int EndColumnIndex { get; set; }
        public int SeatCapability { get; set; }
        public int TableType { get; set; }
        //1 square, 2 rectangle, 3 round
    }
}
