using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Appellation.Controllers
{
    public class Login : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Challenge()
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/home"
            };

            return Challenge(properties, "Spotify");
        }
    }   
}
