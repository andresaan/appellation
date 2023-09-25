using Data.Seed;
using Microsoft.AspNetCore.Mvc;
using Spotify.Services;
using Application.Interfaces;
using Application.SongRecLogic;
using Appellation.Models;

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
        public IActionResult Index(SeedVerificationModel model)
        {

            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Verification(SongRecSeed model)
        {
            model = await _songRecommendationLogic.VerifySeedInputsAsync(model);

            var seedVerificationModel = new SeedVerificationModel();
            seedVerificationModel.Intermediaries = model.SeedIntermediaries;

            return View(seedVerificationModel);
        }
    }


}
