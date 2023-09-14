using Microsoft.AspNetCore.Mvc;

namespace Appellation.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
