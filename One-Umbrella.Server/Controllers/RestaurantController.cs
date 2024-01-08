using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects.Mappers;

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
            if (images.Count() > 0)
            {
                IEnumerable<ImageRestaurantDTO> convertedImages = images.Select(i => i.ToDTO());
                return Ok(restaurant.ToDTO(convertedImages));
            }
            return NotFound();
        }

        [HttpGet("nameOrAddress")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetByIdentifier(string name)
        {
            Restaurant? restaurant = _restaurantService.getRestaurantByIdentifier(name);
            if (restaurant == null)
            {
                return NotFound();
            }
            IEnumerable<ImageRestaurant>? images = _imageService.getAllForOneRestaurant(restaurant.RestaurantId);
            if(images.Count() > 0)
            {
                IEnumerable<ImageRestaurantDTO> convertedImages = images.Select(i => i.ToDTO());
            return Ok(restaurant.ToDTO(convertedImages));
            }
            return NotFound();
        }
    }
}
