using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneUmbrella.BLL.Interfaces;
using OneUmbrella.Domain.Entities;
using OneUmbrella.Server.DataTransferObjects;
using OneUmbrella.Server.DataTransferObjects.Mappers;
using OneUmbrella.Server.Services;

namespace OneUmbrella.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IUserService _userService;
        TokenService _tokenService;

        public AuthController(IUserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthTokenDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Login([FromBody] AuthLoginDataDTO loginDataDTO)
        {
            Human? user = _userService.login(loginDataDTO.HumanIdentifier, loginDataDTO.HumanPassword);
            if(user == null)
            {
                return ValidationProblem();
            }
            return Ok(new AuthTokenDTO()
            {
                Token = _tokenService.GenerateJwt(user),
                User = user.ToDTO()
            });
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] AuthRegisterDataDTO registerDataDTO)
        {
            int? userId = _userService.register(new Human()
            {
                HumanLastName = registerDataDTO.HumanLastName,
                HumanFirstName = registerDataDTO.HumanFirstName,
                HumanMail = registerDataDTO.HumanMail,
                HumanPassword = registerDataDTO.HumanPassword,
                HumanPhoneNumber = registerDataDTO.HumanPhoneNumber,
                HumanType = registerDataDTO.HumanType,
            });

            return userId != null ? Ok(userId) : BadRequest();
        }
    }
}
