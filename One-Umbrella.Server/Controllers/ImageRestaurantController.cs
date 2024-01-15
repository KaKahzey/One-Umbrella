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
    public class ImageRestaurantController : ControllerBase
    {
        IImageRestaurantService _imageRestaurantService;

        public ImageRestaurantController(IImageRestaurantService imageRestaurantService)
        {
            _imageRestaurantService = imageRestaurantService;
        }

        [HttpGet("GetAllForOneRestaurant/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ImageRestaurantDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getImages([FromRoute] int id)
        {
            IEnumerable<ImageRestaurantDTO>? images = _imageRestaurantService.getAllForOneRestaurant(id).Select(i => i.ToDTO());
            if (images.Count() < 1)
            {
                return NotFound();
            }
            return Ok(images);
        }

        [HttpGet("GetFrontImage/{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ImageRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult getFrontImage([FromRoute] int id)
        {
            ImageRestaurantDTO? image = _imageRestaurantService.getFrontImage(id).ToDTO();
            return Ok(image);
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(ImageRestaurantDataDTO image)
        {
            int newImageId = _imageRestaurantService.create(image.ToEntity());
            return newImageId != null ? Ok(newImageId) : BadRequest();
        }

        [HttpDelete("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete([FromRoute] int id)
        {
            bool isDeleted = _imageRestaurantService.delete(id);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
