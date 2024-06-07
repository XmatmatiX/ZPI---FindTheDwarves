using FindTheDwarves.Application.DTO.Dwarves;
using FindTheDwarves.Application.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace FindTheDwarvesWeb.Controllers
{
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            string role = Request.Cookies["Role"];

            if (role != "Admin")
            {
                return RedirectToAction("RoleError", "Error");
            }

            return View();
        }

        public async Task<IActionResult> Users()
        {
            string role = Request.Cookies["Role"];

            if (role != "Admin")
            {
                return RedirectToAction("RoleError", "Error");
            }

            ListShowUserDTO result = new ListShowUserDTO();

            var token = Request.Cookies["JWTCookie"];

            string uri = "https://localhost:7007/api/Admin/Users/"; 

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                using (var response = await httpClient.GetAsync(uri))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RoleError", "Error");
                    }

                    var apiResponse = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<ListShowUserDTO>(apiResponse);
                }

            }

            return View(result.UserList);
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            string role = Request.Cookies["Role"];

            if (role != "Admin")
            {
                return RedirectToAction("RoleError", "Error");
            }

            var token = Request.Cookies["JWTCookie"];

            string uri = "https://localhost:7007/api/Admin/DeleteUser/" + id.ToString();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                using (var response = await httpClient.PostAsync(uri,null))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RoleError", "Error");
                    }
                }
            }

            return RedirectToAction("Users");

        }

        public async Task<IActionResult> Dwarves()
        {
            string role = Request.Cookies["Role"];

            if (role != "Admin")
            {
                return RedirectToAction("RoleError", "Error");
            }

            ListShowDwarvesDTO result = new ListShowDwarvesDTO();

            var token = Request.Cookies["JWTCookie"];

            string uri = "https://localhost:7007/api/Dwarf/GetDwarves/";

            using (var httpClient = new HttpClient())
            {

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                using (var response = await httpClient.GetAsync(uri))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RoleError", "Error");
                    }

                    var apiResponse = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<ListShowDwarvesDTO>(apiResponse);
                }

            }

            return View(result.DwarfList);
        }

        public async Task<IActionResult> DeleteDwarf(int id)
        {

            string role = Request.Cookies["Role"];

            if (role != "Admin")
            {
                return RedirectToAction("RoleError", "Error");
            }

            var token = Request.Cookies["JWTCookie"];

            string uri = "https://localhost:7007/api/Admin/DeleteDwarf/" + id.ToString();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

                using (var response = await httpClient.PostAsync(uri, null))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("RoleError", "Error");
                    }
                }
            }

            return RedirectToAction("Dwarves");
        }

        public async Task<IActionResult> EditDwarf(int id)
        {
            var token = Request.Cookies["JWTCookie"];
            UpdateDwarfDTO result;
            string uri = "https://localhost:7007/api/Admin/GetDwarfToEdit/" + id.ToString();

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                using (var response = await httpClient.GetAsync(uri))
                {

                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("login", "account");
                    }

                    var apiResponse = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<UpdateDwarfDTO>(apiResponse);
                }


            }

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditDwarf(UpdateDwarfDTO data)
        {

            var token = Request.Cookies["JWTCookie"];

            string uri = "https://localhost:7007/api/Admin/UpdateDwarf";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var respone = await httpClient.PostAsync(uri, content))
                {

                    if (respone.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("login", "Account");
                    }

                    string apiResponse = await respone.Content.ReadAsStringAsync();
                }
            }


            return RedirectToAction("Dwarves");
        }

        public IActionResult CreateDwarf()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateDwarf(NewDwarfDTO data)
        {
            var token = Request.Cookies["JWTCookie"];

            string uri = "https://localhost:7007/api/Admin/AddDwarf";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                using (var respone = await httpClient.PostAsync(uri, content))
                {
                    if (respone.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return RedirectToAction("login", "Account");
                    }
                }
            }

            return RedirectToAction("Dwarves");
        }
    }
}
