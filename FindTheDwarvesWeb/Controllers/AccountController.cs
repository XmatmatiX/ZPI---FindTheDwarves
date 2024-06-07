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
        public async Task<IActionResult> Login(LoginDTO user)
        {
            string uri = "https://localhost:7007/api/account/login";

            LoginResponseDTO userData;

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(uri, content))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var apiResponse = await response.Content.ReadAsStringAsync();

                        userData = JsonConvert.DeserializeObject<LoginResponseDTO>(apiResponse);

                        var cookieOptions = new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTimeOffset.UtcNow.AddDays(3)
                        };

                        Response.Cookies.Append("JWTCookie", userData.JWT, cookieOptions);
                        Response.Cookies.Append("Username", userData.Username, cookieOptions);
                        Response.Cookies.Append("Role", userData.RoleName, cookieOptions);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.LoginResponse = await response.Content.ReadAsStringAsync();
                        return View();
                    }

                }
            }
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

        }

        public IActionResult Logout()
        {

            Response.Cookies.Delete("JWTCookie");
            Response.Cookies.Delete("Username");
            Response.Cookies.Delete("Role");

            return RedirectToAction("Index", "Home");
        }
    }
}
