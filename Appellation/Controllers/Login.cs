using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Appellation.Controllers
{
    public class Login : Controller
    {
        // User interacted controller and the button in view will need to send user to my endpoint
        public IActionResult Index()
        {
            return View();
        }

        // Move to seperate entity auth controller
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
