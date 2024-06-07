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
        public ActionResult GetDwarves()
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

            if (result == null)
            {
                return BadRequest($"Kod {dto.ActivationCode} nie pasuje do żadnego krasnala");
            }

            return Ok(result);
        }

        [HttpGet("DwarfDetails/{name}")]
        [Authorize]
        public ActionResult GetDwarfDetails(string name)
        {
            var userID = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = _dwarfService.GetDwarfByName(name, userID);

            if (result == null)
            {
                return BadRequest($"Krasnal {name} nie istnieje w bazie danych lub nie masz do niego prawa dostępu");
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
