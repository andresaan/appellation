using Data.Seed;
using Microsoft.AspNetCore.Mvc;
using Spotify.Services;

namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {
        private ISeedService _seedService;
        public SongRecommendationsController(ISeedService seedService)
        {
            _seedService = seedService;
        
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Verification(SeedInput model)
        {
            _seedService.ProcessSeeds(model);

            return View();
        }
    }


}
