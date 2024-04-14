using FindTheDwarves.Application.DTO.Comments;
using FindTheDwarves.Application.DTO.Dwarves;
using FindTheDwarves.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FindTheDwarvesAPI.Controllers
{
    [Route("api/Dwarf")]
    [ApiController]
    public class DwarfController : ControllerBase
    {
        private readonly IDwarfService _dwarfService;
        public DwarfController(IDwarfService dwarfService)
        {
            _dwarfService = dwarfService;
        }

        [HttpPost("AddComment")]
        [Authorize]
        public ActionResult AddComment([FromBody] NewCommentDTO dto)
        {
            int userID = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = _dwarfService.AddComment(dto, userID);

            if (result == -1)
            {
                return BadRequest("Krasnal o podanym ID nie istnieje");
            }
            if (result == -2)
            {
                return BadRequest("Posiadacz klucza nie ma uprawnień do komentowania tego krasnala");
            }

            return Ok(result);

        }

        [HttpGet("GetDwarves")]
        public ActionResult GetDwarf()
        {
            var dwarves = _dwarfService.GetDwarves();

            return Ok(dwarves);

        }

        [HttpPost("ClaimDwarf")]
        [Authorize]
        public ActionResult ClaimDwarf([FromBody] ClaimDwarfDTO dto)
        {
            int userID = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = _dwarfService.ClaimDwarf(dto, userID);

            if (result == -1)
            {
                return BadRequest($"Kod {dto.ActivationCode} nie pasuje do żadnego krasnala");
            }
            if (result == -2)
            {
                return BadRequest($"Krasnal został już odblokowany");
            }

            return Ok(result);
        }

        [HttpPost("DwarfDetails")]
        [Authorize]
        public ActionResult GetDwarfDetails([FromBody] SearchForDwarfDTO dto)
        {

            var result = _dwarfService.GetDwarfByName(dto.Name);

            if (result == null)
            {
                return BadRequest($"Krasnal {dto.Name} nie istnieje w bazie danych");
            }

            return Ok(result);
        }

        [HttpGet("VisitedDwarves")]
        [Authorize]
        public ActionResult VisitedDwarves()
        { 
            var userID = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = _dwarfService.GetVisitedDwarves(userID);

            return Ok(result);

        }


    }
}
