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
    public class FavoriteController : ControllerBase
    {

        private IFavoriteService _favoriteService;
        private IRestaurantService _restaurantService;

        public FavoriteController(IFavoriteService favoriteService, IRestaurantService restaurantService)
        {
            _favoriteService = favoriteService;
            _restaurantService = restaurantService;
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FavoriteDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllFavoritesForOneUser([FromRoute] int id)
        {
            IEnumerable<FavoriteDTO> favorites = _favoriteService.getAllForHuman(id).Select(f => f.ToDTO(id));
            return favorites.Any() ? Ok(favorites) : NotFound();
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(int humanId, int restaurantId)
        {
            Favorite newFavorite = FavoriteMapper.ToEntity(humanId, restaurantId);

            return Ok(_favoriteService.create(newFavorite));
        }

        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int humanId, int restaurantId)
        {
            return _favoriteService.delete(humanId, restaurantId) ? Ok() : NotFound();
        }
    }
}
