using Appellation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Appellation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAuthenticationService _authenticationService;

        public HomeController(ILogger<HomeController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        
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

        public RedirectToActionResult LogOut()
        {
            _authenticationService.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme, null);

            return RedirectToAction("LogIn");
        }
    }
}