using Microsoft.AspNetCore.Mvc;

namespace FindTheDwarvesWeb.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult RoleError()
        {
            return View();
        }

        public IActionResult NotClaimed() 
        {
            return View();
        }
    }
}
