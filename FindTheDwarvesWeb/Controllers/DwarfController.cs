using FindTheDwarves.Application.DTO.Dwarves;
using FindTheDwarves.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace FindTheDwarvesWeb.Controllers
{
    public class DwarfController : Controller
    {

        public async Task<IActionResult> Index()
        {

            string uri = "https://localhost:7007/api/Dwarf/GetDwarves";
            
            ListShowDwarvesDTO result = new ListShowDwarvesDTO();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(uri))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ListShowDwarvesDTO>(apiResponse);
                    }
                }
            }
            catch (Exception)
            {

                return RedirectToAction("Login", "Account");
            }
            

            return View(result.DwarfList);
        }

        public async Task<IActionResult> MyDwarves()
        {
            string uri = "https://localhost:7007/api/Dwarf/VisitedDwarves";

            var token = Request.Cookies["JWTCookie"];

            ListShowDwarvesDTO result = new ListShowDwarvesDTO();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.GetAsync(uri))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ListShowDwarvesDTO>(apiResponse);
                    }

                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
            }

            return View(result.DwarfList);
        }

        public IActionResult ClaimDwarf()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ClaimDwarf(ClaimDwarfDTO data)
        {
            var token = Request.Cookies["JWTCookie"];
            
            string uri = "https://localhost:7007/api/Dwarf/ClaimDwarf";

            DwarfDetailsDTO result;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var respone = await httpClient.PostAsync(uri, content))
                {
                    if (respone.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.ClaimResponse = "Nie istnieje taki kod!";
                        return View();
                    }

                    if (respone.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("login", "Account");
                    }

                    string apiResponse = await respone.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<DwarfDetailsDTO>(apiResponse);

                    return RedirectToAction("DwarfDetails", new { dwarfName = result.Name });
                }
            }
        }

        public async Task<IActionResult> DwarfDetails(string dwarfName)
        {

            var token = Request.Cookies["JWTCookie"];
            DwarfDetailsDTO result;
            string uri = "https://localhost:7007/api/Dwarf/DwarfDetails/"+dwarfName;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.GetAsync(uri))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("login", "account");
                    }

                    if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.DetailsResponse = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("NotClaimed", "Error");
                    }

                    var apiResponse = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<DwarfDetailsDTO>(apiResponse);
                }


            }
            
            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> DwarfDetails(DwarfDetailsDTO data)
        {

            Console.WriteLine();

            return RedirectToAction("DwarfDetails", data.Name);
        }

    }
}
