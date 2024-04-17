using FindTheDwarves.Application.DTO.Dwarves;
using FindTheDwarves.Application.DTO.Users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FindTheDwarvesWeb.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginDTO user)
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO user)
        {

            string uri = "https://localhost:7007/api/account/register";

            string result;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(uri, content))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.RegisterResponse = await response.Content.ReadAsStringAsync();
                        return View();
                    }
                    
                }
            }

            return View();


        }
    }
}
