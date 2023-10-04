using Application.Interfaces;
using Application.Dtos;
using Appellation.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<IActionResult> Index(SeedVerificationModel model)
        {
            var songRecommendationsIndexModel = new SongRecommendationsIndexModel()
            {
                Tracks = await _processSongRecommendations.GetSongRecommendationsAsync(new SeedVerificationDto()
                {
                    ArtistVerifiedSeeds = model.ArtistVerifiedSeeds,
                    TrackVerifiedSeeds = model.TrackVerifiedSeeds,

                    TArtistVerifiedSeeds = model.TArtistVerifiedSeeds
                }),

                RecommendationsGiven = true
            };

            return View(songRecommendationsIndexModel);
        }

        [HttpPost]
        public async Task<IActionResult> Verification(SongRecommendationsIndexModel model)
        {
            var seedVerificationModelDto = await _processSongRecommendations.VerifySeedInputsAsync(new SongRecommendationsDto()
            {
                ArtistUserInput = model.ArtistUserInput,
                GenreUserInput = model.GenreUserInput,
                TrackUserInput = model.TrackUserInput,
                Tracks = model.Tracks,
                RecommendationsGiven = model.RecommendationsGiven
            });

            var seedVerificationModel = new SeedVerificationModel()
            {
                SeedIntermediaries = seedVerificationModelDto.SeedIntermediaries,
                TrackSeedIntermediaries = seedVerificationModelDto.TrackSeedIntermediaries,
                ArtistVerifiedSeeds = seedVerificationModelDto.ArtistVerifiedSeeds,
            }; 

            return View(seedVerificationModel);
        }
    }


}
