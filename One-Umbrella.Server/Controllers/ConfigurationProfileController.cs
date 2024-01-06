using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects;
using System.Diagnostics;
using OneUmbrella.Server.DataTransferObjects.Mappers;

namespace OneUmbrella.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationProfileController : ControllerBase
    {
        IUserService _userService;

        public ConfigurationProfileController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfigurationProfileDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetEvent([FromRoute] int id)
        {
            Human? user = _userService.getUser(id);

            if (user is null)
            {
                return NotFound();
            }
            return Ok(user.ToDTO());
        }


        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ConfigurationProfileDataDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromRoute] int id, [FromBody] ConfigurationProfileDataDTO userDTO)
        {
            Human? user = _userService.getUser(id);
            if(user == null)
            {
                return NotFound();
            }
            if (userDTO.HumanId != user.HumanId)
            {
                return Unauthorized();
            }
            user.HumanLastName = userDTO.HumanLastName;
            user.HumanFirstName = userDTO.HumanFirstName;
            user.HumanMail = userDTO.HumanMail;
            user.HumanPassword = userDTO.HumanPassword;
            user.HumanPhoneNumber = userDTO.HumanPhoneNumber;
            _userService.updateUser(id, user);
            return Ok();
        }
    }
}
