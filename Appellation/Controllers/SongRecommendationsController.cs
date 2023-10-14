using Application.Interfaces;
using Appellation.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Seed;

namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {
        private ISongRecommendationHandler _processSongRecommendations;
        public SongRecommendationsController(ISongRecommendationHandler songRecommendationLogic)
        {
            _processSongRecommendations = songRecommendationLogic;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var model = new SongRecommendationsIndexModel();

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Index(SongRecommendationsIndexModel model)
        //{
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Index(SeedVerificationModel model)
        {

            var songRecommendationsIndexModel = new SongRecommendationsIndexModel()
            {
                Tracks = await _processSongRecommendations.GetSongRecommendationsAsync(
                    model.ArtistVerifiedSeeds, model.TrackVerifiedSeeds, model.GenreVerifiedSeeds),

                RecommendationsGiven = true
            };

            return View(songRecommendationsIndexModel);
        }

        [HttpPost]
        public async Task<IActionResult> Verification(SongRecommendationsIndexModel model)
        {
            var songRecommendationSeeds = await _processSongRecommendations.VerifySeedInputsAsync(model.ArtistUserInput, model.TrackUserInput, model.GenreUserInput);

            var seedVerificationModel = new SeedVerificationModel()
            {
                SeedIntermediaries = songRecommendationSeeds.SeedIntermediaries,
                TrackSeedIntermediaries = songRecommendationSeeds.TrackSeedIntermediaries,
                GenreVerifiedSeeds = songRecommendationSeeds.GenreVerifiedSeeds,
            };

            return View(seedVerificationModel);
        }

    }
}
