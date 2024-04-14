using FindTheDwarves.Application.DTO.Users;
using FindTheDwarves.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindTheDwarvesAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDTO dto)
        {
            int result = _userService.RegisterUser(dto);
            if (result == -1)
                return BadRequest("Email already taken");
            if (result == -2)
                return BadRequest("Username already taken");
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO dto)
        {

            string token;

            try
            {
                token = _userService.GenerateJWT(dto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok(token);
        }


    }
}
