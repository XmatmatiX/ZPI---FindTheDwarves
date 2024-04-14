using FindTheDwarves.Application.DTO.Dwarves;
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

        [HttpPost("AddDwarf")]
        public ActionResult AddDwarf([FromBody] NewDwarfDTO dto)
        {

            var result = _dwarfService.AddNewDwarf(dto);

            if (result == -1)
                return Conflict("Dwarf name is already used in database");
            if (result == -2)
                return Conflict("Activation code is already used in database");
            return Ok(result);

        }

    }
}
