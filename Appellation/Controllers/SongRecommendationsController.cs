using Application.Interfaces;
using Appellation.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Seed;
using Microsoft.AspNetCore.Authorization;

namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {
        private ISongRecommendationHandler _processSongRecommendations;
        public SongRecommendationsController(ISongRecommendationHandler songRecommendationLogic)
        {
            _processSongRecommendations = songRecommendationLogic;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(SeedVerificationModel model)
        {

            var songRecommendationsIndexModel = new SongRecommendationsIndexModel()
            {
                Tracks = await _processSongRecommendations.GetSongRecommendationsAsync(
                    model.ArtistVerifiedSeeds, model.TrackVerifiedSeeds, model.GenreVerifiedSeeds, model.Limit),

                RecommendationsGiven = true
            };

            return View(songRecommendationsIndexModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Verification(SongRecommendationsIndexModel model)
        {
            var songRecommendationSeeds = await _processSongRecommendations.VerifySeedInputsAsync(model.ArtistUserInput, model.TrackUserInput, model.GenreUserInput);

            var seedVerificationModel = new SeedVerificationModel()
            {
                ArtistSeedIntermediaries = songRecommendationSeeds.SeedIntermediaries,
                TrackSeedIntermediaries = songRecommendationSeeds.TrackSeedIntermediaries,
                GenreVerifiedSeeds = songRecommendationSeeds.GenreVerifiedSeeds,
                Limit = model.Limit
            };

            return View(seedVerificationModel);
        }

    }
}
