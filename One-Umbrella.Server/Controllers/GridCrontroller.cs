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

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(GridDataDTO grid)
        {
            Grid newGrid = grid.ToEntity();
            int createdGridId = _gridService.create(newGrid);
            IEnumerable<TableEntity> tables = grid.GridTables.Select(t => { t.GridId = createdGridId; return t; });
            IEnumerable<StructuralElement> elements = grid.GridElements.Select(e => { e.GridId = createdGridId; return e; });
            foreach(TableEntity t in tables)
            {
                _tableService.create(t);
            }
            foreach(StructuralElement e in elements)
            {
                _elementService.create(e);
            }
            return createdGridId != null ? Ok(createdGridId) : BadRequest();
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] int id, GridDataDTO grid)
        {
            IEnumerable<TableEntity>? tables = grid.GridTables;
            IEnumerable<StructuralElement>? elements = grid.GridElements;
            if(_gridService.getById(id) == null)
            {
                return NotFound();
            }

            foreach(StructuralElement e in _elementService.getAllForOneGrid(id))
            {
                _elementService.delete(e.ElementId);
            }

            foreach(TableEntity t in tables)
            {
                if(t.GridId != grid.GridId)
                {
                    return BadRequest();
                }
                if(_tableService.getById(t.TableId) != null)
                {
                    _tableService.update(t.TableId, t);
                }
                else
                {
                    _tableService.create(t);
                }
            }
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
            IEnumerable<TableEntity>? tables = _tableService.getAllForOneGrid(id);
            IEnumerable<StructuralElement>? elements = _elementService.getAllForOneGrid(id);
            foreach(TableEntity t in tables)
            {
                _tableService.delete(t.TableId);
            }
            foreach (StructuralElement e in elements)
            {
                _elementService.delete(e.ElementId);
            }
            return _gridService.delete(id) ? Ok() : NotFound();
        }
    }
}
