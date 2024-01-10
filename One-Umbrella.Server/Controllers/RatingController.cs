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
    public class RatingController : ControllerBase
    {
        IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet("GetAllForOneRestaurant/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RatingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getAllForOneRestaurant([FromRoute] int id)
        {
            IEnumerable<RatingDTO> ratings = _ratingService.getAllByRestaurant(id, false).Select(r => r.ToDTO());
            return ratings.Any() ? Ok(ratings) : NotFound();
        }

        [HttpGet("GetAllForOneUser/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RatingDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult getAllForOneUser([FromRoute] int id)
        {
            IEnumerable<RatingDTO> ratings = _ratingService.getAllByRestaurant(id, true).Select(r => r.ToDTO());
            return ratings.Any() ? Ok(ratings) : NotFound();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(RatingDataDTO rating)
        {
             bool result = _ratingService.create(rating.ToEntity());
            return result ? Ok() : BadRequest();
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] int id, RatingDataDTO rating)
        {
            Rating changedRating = rating.ToEntity();
            return _ratingService.update(changedRating) ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int humanId, int restaurantId)
        {
            return _ratingService.delete(humanId, restaurantId) ? Ok() : NotFound();
        }
    }
}
