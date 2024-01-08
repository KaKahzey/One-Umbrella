using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.BLL.Services;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Server.DataTransferObjects.Mappers;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ListRestaurantDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetListRestaurants(int page, int pageSize, string sortBy, bool isDescending, int? humanId, string? city)
        {
            List<ListRestaurantDTO> restaurantsDTO = new List<ListRestaurantDTO>();
            IEnumerable<Restaurant>? restaurants = _restaurantService.getListRestaurants(page, pageSize, sortBy, isDescending, humanId, city);
            foreach(Restaurant r in restaurants)
            {
                if(_imageService.getFrontImage(r.RestaurantId) != null)
                {
                    string image = _imageService.getFrontImage(r.RestaurantId).ToDTO().ImageData;
                    restaurantsDTO.Add(ListRestaurantMapper.ToDTO(r, image));
                }
                else
                {
                    restaurantsDTO.Add(ListRestaurantMapper.ToDTO(r, "")); ;
                }
            }
            IEnumerable<ListRestaurantDTO> convertedRestaurants = restaurantsDTO;
            return Ok(convertedRestaurants);
        }

        

    }
}
