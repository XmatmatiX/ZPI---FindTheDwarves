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

        [HttpGet("GetDwarfToEdit/{id}")]
        public ActionResult GetDwarfToEdit(int id)
        {
            var result = _dwarfService.GetDwarfToEdit(id);

            return Ok(result);
        }

        [HttpPost("UpdateDwarf")]
        public ActionResult UpdateDwarf([FromBody] UpdateDwarfDTO dto)
        {
            _dwarfService.UpdateDwarf(dto);
            return Ok();
        }

        [HttpGet("Users")]
        public ActionResult GetUsers()
        {

            var result = _userService.GetUsers();

            return Ok(result);

        }

        [HttpPost("DeleteDwarf/{id}")]
        public ActionResult DeleteDwarf(int id)
        {
            _dwarfService.DeleteDwarf(id);

            return Ok();
        }


        [HttpPost("DeleteUser/{id}")]
        public ActionResult DeleteUser(int id) 
        {
            _userService.DeleteUser(id);

            return Ok();
        }

    }
}
