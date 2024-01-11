using OneUmbrella.Domain.Entities;


namespace OneUmbrella.Server.DataTransferObjects.Mappers
{
    public static class GridMapper
    {
        public static GridDTO ToDTO(this Grid grid, IEnumerable<TableEntity> tables, IEnumerable<StructuralElement> elements)
        {
            return new GridDTO()
            {
                RestaurantId = grid.RestaurantId,
                GridId = grid.GridId,
                GridName = grid.GridName,
                GridRows = grid.GridRows,
                GridColumns = grid.GridColumns,
                GridTables = tables,
                GridElements = elements
            };
        }
        public static Grid ToEntity(this GridDataDTO gridDataDTO)
        {
            return new Grid()
            {
                RestaurantId = gridDataDTO.RestaurantId,
                GridName = gridDataDTO.GridName,
                GridRows = gridDataDTO.GridRows,
                GridColumns = gridDataDTO.GridColumns
            };
        }
    }
}
