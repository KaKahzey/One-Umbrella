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
        public IActionResult getUser([FromRoute] int id)
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
        public IActionResult Update([FromRoute] int id, [FromBody] ConfigurationProfileDataDTO userDataDTO)
        {
            Human? user = _userService.getUser(id);
            if(user == null)
            {
                return NotFound();
            }
            user.HumanLastName = userDataDTO.HumanLastName;
            user.HumanFirstName = userDataDTO.HumanFirstName;
            user.HumanMail = userDataDTO.HumanMail;
            user.HumanPassword = userDataDTO.HumanPassword;
            user.HumanPhoneNumber = userDataDTO.HumanPhoneNumber;
            _userService.updateUser(user.HumanId, userDataDTO.ToEntity()); ;
            return Ok();
        }
    }
}
