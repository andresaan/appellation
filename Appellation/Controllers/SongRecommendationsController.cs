using Data.Seed;
using Microsoft.AspNetCore.Mvc;
using Spotify.Services;
using Application.Interfaces;
using Application.SongRecLogic;

namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {
        private ISongRecommendationLogic _songRecommendationLogic;
        public SongRecommendationsController(ISongRecommendationLogic songRecommendationLogic)
        {
            _songRecommendationLogic = songRecommendationLogic;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SeedIntermediary model)
        {

            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Verification(SongRecSeed model)
        {
            model = await _songRecommendationLogic.VerifySeedInputsAsync(model);

            return View(model);
        }
    }


}
