using Data.Seed;
using Microsoft.AspNetCore.Mvc;
using Spotify.Services;
using Application.Interfaces;
using Application.SongRecLogic;
using Application.Models.SongRecommendations;

namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {
        private IProcessSongRecommendations _songRecommendationLogic;
        public SongRecommendationsController(IProcessSongRecommendations songRecommendationLogic)
        {
            _songRecommendationLogic = songRecommendationLogic;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new SongRecommendationsIndexModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(SeedVerificationModel model)
        {
            var songRecommendationsIndexModel = new SongRecommendationsIndexModel();
            // potential seed types should be filled here
            songRecommendationsIndexModel.Tracks = await _songRecommendationLogic.GetSongRecommendationsAsync(model);

            songRecommendationsIndexModel.RecommendationsGiven = true;

            return View(songRecommendationsIndexModel);
        }

        //[HttpPost]
        //public async Task<IActionResult> Verification(SongRecSeed model)
        //{
        //    model = await _songRecommendationLogic.VerifySeedInputsAsync(model);

        //    var seedVerificationModel = new SeedVerificationModel();
        //    seedVerificationModel.Intermediaries = model.SeedIntermediaries;

        //    return View(seedVerificationModel);
        //}

        [HttpPost]
        public async Task<IActionResult> Verification(SongRecommendationsIndexModel model)
        {
            var seedVerificationModel = await _songRecommendationLogic.VerifySeedInputsAsync(model);

            return View(seedVerificationModel);
        }
    }


}
