using FindTheDwarves.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FindTheDwarvesAPI.Controllers
{
    [Route("api/Admin")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDwarfService _dwarfService;
        private readonly IAchievementService _achievementService;
        public AdminController(IUserService userService,IDwarfService dwarfService, IAchievementService achievementService)
        {
            _userService = userService;
            _dwarfService = dwarfService;
            _achievementService = achievementService;
        }


        [HttpGet("test")]
        public ActionResult Test() 
        {
            return Ok("test udany");
        }
    }
}
