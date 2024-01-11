using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.BLL.Services;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Server.DataTransferObjects.Mappers;
using System.Collections.Generic;

namespace OneUmbrella.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        IReservationService _reservationService;
        IReservedTableService _reservedTableService;
        IRestaurantService _restaurantService;

        public ReservationController(IReservationService reservationService, IReservedTableService reservedTable, IRestaurantService restaurantService)
        {
            _reservationService = reservationService;
            _reservedTableService = reservedTable;
            _restaurantService = restaurantService;
        }

        [HttpGet("GetAllForOneRestaurant/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getAllForOneRestaurant([FromRoute] int id, DateTime date)
        {
            IEnumerable<ReservationDTO> reservations = _reservationService.getAllForOneRestaurantForOneDay(id, date).Select(r => r.ToDTO());
            IEnumerable<ReservedTable> tablesReservees = _reservedTableService.getAllForOneRestaurantForOneDay(id, date);
            WholeReservationDTO wholeReservations = new WholeReservationDTO
            {
                Reservations = reservations,
                ReservedTables = tablesReservees
            };
            return Ok(wholeReservations);
        }

        [HttpGet("GetAllForOneUser/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getAllForOneUser([FromRoute] int id)
        {
            IEnumerable<ReservationForUserDTO> reservations = _reservationService.getAllForOneHuman(id).Select(r => r.ToDTO(_restaurantService.getRestaurantById(r.RestaurantId)));
            return Ok(reservations);
        }

        [HttpGet("getAllByStatus/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ReservationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getAllByStatus([FromRoute] int id, int status)
        {
            IEnumerable<ReservationDTO> reservations = _reservationService.getAllByStatus(id, status).Select(r => r.ToDTO());
            return Ok(reservations);
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] int id, int status)
        {
            bool result = _reservationService.changeStatus(id, status);
            return result ? Ok() : BadRequest();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(ReservationDataDTO reservation, int tableId)
        {
            int createdReservationId = _reservationService.create(reservation.ToEntity());
            return createdReservationId != null ? Ok(_reservedTableService.create(createdReservationId, tableId)) : BadRequest();
        }

        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] int id)
        {
            return _reservationService.delete(id) ? Ok() : NotFound();
        }
    }
}
