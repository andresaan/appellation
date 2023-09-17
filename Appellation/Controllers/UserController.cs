using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Appellation.Controllers
{
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }

        new public IActionResult Challenge()
        {
            var properties = new AuthenticationProperties()
            {
                RedirectUri = "/home"
            };

            return Challenge(properties, "Spotify");
        }

        public IActionResult LogOut() 
        { 
            return View(); 
        }

    }   
}
