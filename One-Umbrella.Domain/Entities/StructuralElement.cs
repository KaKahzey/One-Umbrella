using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneUmbrella.Domain.Entities
{
    public class StructuralElement
    {
        public int ElementId { get; set; }
        public int GridId { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public int ElementType { get; set; }
        /* 1 pour mur
         * 2 pour fenêtre
         * 3 pour escalier
         * 4 pour porte
         * 5 pour porte des toilettes
         * 6 pour porte des cuisines
         * 7 pour bar
         * 8 pour tv
         */

    }
}
