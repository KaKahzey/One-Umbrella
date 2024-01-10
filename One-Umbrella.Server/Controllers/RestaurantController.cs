using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects.Mappers;
using OneUmbrella.BLL.Services;

namespace OneUmbrella.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        IRestaurantService _restaurantService;
        IImageRestaurantService _imageService;

        public RestaurantController(IRestaurantService restaurantService, IImageRestaurantService imageService)
        {
            _restaurantService = restaurantService;
            _imageService = imageService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetById(int id)
        {
            Restaurant? restaurant = _restaurantService.getRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            IEnumerable<ImageRestaurant>? images = _imageService.getAllForOneRestaurant(restaurant.RestaurantId);
            IEnumerable<ImageRestaurantDTO> convertedImages = images.Select(i => i.ToDTO());
            return Ok(restaurant.ToDTO(convertedImages));
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(RestaurantDataDTO restaurant)
        {
            Restaurant convertedRestaurant = restaurant.ToEntity();
            if(convertedRestaurant == null)
            {
                return BadRequest();
            }
            return Ok(_restaurantService.createRestaurant(convertedRestaurant));
        }

        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromRoute] int id, RestaurantDataDTO restaurant)
        {
            Restaurant changedRestaurant = restaurant.ToEntity();
            return _restaurantService.updateRestaurant(id, changedRestaurant) ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromRoute] int id)
        {
            return _restaurantService.deleteRestaurant(id) ? Ok() : NotFound();
        }
    }
}
