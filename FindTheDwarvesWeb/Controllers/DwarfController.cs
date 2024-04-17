using FindTheDwarves.Application.DTO.Dwarves;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FindTheDwarvesWeb.Controllers
{
    public class DwarfController : Controller
    {

        public async Task<IActionResult> Index()
        {

            string uri = "https://localhost:7007/api/Dwarf/GetDwarves";
            
            ListShowDwarvesDTO result = new ListShowDwarvesDTO();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(uri))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ListShowDwarvesDTO>(apiResponse);
                }
            }

            return View(result.DwarfList);
        }
    }
}
