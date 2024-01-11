using OneUmbrella.Domain.Entities;

namespace OneUmbrella.Server.DataTransferObjects
{
    public class GridDTO
    {
        public int RestaurantId { get; set; }
        public int GridId { get; set; }
        public string GridName { get; set; }
        public int GridRows { get; set; }
        public int GridColumns { get; set; }
        public IEnumerable<TableEntity> GridTables { get; set; }
        public IEnumerable<StructuralElement> GridElements { get; set; }

    }

    public class GridDataDTO
    {
        public int RestaurantId { get; set; }
        public int? GridId { get; set; }
        public string GridName { get; set; }
        public int GridRows { get; set; }
        public int GridColumns { get; set; }
        public IEnumerable<TableEntity> GridTables { get; set; }
        public IEnumerable<StructuralElement> GridElements { get; set; }
    }
}
