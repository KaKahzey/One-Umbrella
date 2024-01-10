using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.BLL.Services;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Server.DataTransferObjects.Mappers;

namespace OneUmbrella.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GridCrontroller : ControllerBase
    {
        IGridService _gridService;
        ITableEntityService _tableService;
        IStructuralElementService _elementService;

        public GridCrontroller(IGridService gridService, ITableEntityService tableService, IStructuralElementService elementService)
        {
            _gridService = gridService;
            _tableService = tableService;
            _elementService = elementService;
        }

        [HttpGet("GetAllForOneRestaurant/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GridDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getGrids([FromRoute] int id)
        {
            IEnumerable<GridDTO>? grids = _gridService.getAllForOneRestaurant(id).Select(g => g.ToDTO(_tableService.getAllForOneGrid(g.GridId), _elementService.getAllForOneGrid(g.GridId)));
            if (grids.Count() < 1)
            {
                return NotFound();
            }
            return Ok(grids);
        }

        [HttpPost("Create")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(GridDTO grid)
        {
            IEnumerable<TableEntity>? tables = grid.GridTables;
            IEnumerable<StructuralElement>? elements = grid.GridElements;
            Grid newGrid = grid.ToEntity();
            
            tables?.Select(t => _tableService.create(t));
            elements?.Select(e => _elementService.create(e));
            bool createdGrid = _gridService.create(newGrid);
            return createdGrid? Ok() : BadRequest();
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] int id,  GridDTO grid)
        {
            IEnumerable<TableEntity>? tables = grid.GridTables;
            IEnumerable<StructuralElement>? elements = grid.GridElements;

            _elementService.getAllForOneGrid(id).Select(e => _elementService.delete(e.ElementId));

            tables?.Select(t => _tableService.update(t.TableId, t));
            elements?.Select(e => _elementService.create(e));
            return Ok();
        }

        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] int id)
        {
            return _gridService.delete(id) ? Ok() : NotFound();
        }
    }
}
