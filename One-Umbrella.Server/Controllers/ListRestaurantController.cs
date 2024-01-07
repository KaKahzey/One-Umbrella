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
    public class ListRestaurantController : ControllerBase
    {
        IRestaurantService _restaurantService;
        IImageRestaurantService _imageService;

        public ListRestaurantController(IRestaurantService restaurantService, IImageRestaurantService imageService)
        {
            _restaurantService = restaurantService;
            _imageService = imageService;
        }

        [HttpGet("ListRestaurant")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListRestaurantDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetListRestaurants([FromRoute] int page, int pageSize, string sortBy, bool isDescending, int? humanId, string? city)
        {
            IEnumerable<ListRestaurantDTO>? restaurants = _restaurantService.getListRestaurants(page, pageSize, sortBy, isDescending, humanId, city).Select(r => r.ToDTO(_imageService.getFrontImage(r.RestaurantId)));

            return Ok(restaurants);
        }

        
    }
}
