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
            var songRecommendationsIndexModel = new SongRecommendationsIndexModel();
            // potential seed types should be filled here
            songRecommendationsIndexModel.Tracks = await _processSongRecommendations.TestGetSongRecommendationsAsync(new SeedVerificationDto()
            {
                SeedIntermediaries = model.SeedIntermediaries,
                TrackSeedIntermediaries = model.TrackSeedIntermediaries,
                VerifiedSeeds = model.VerifiedSeeds
            });

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
            
            
            var seedVerificationModel = await _processSongRecommendations.VerifySeedInputsAsync(new SongRecommendationsDto()
            {
                ArtistUserInput = model.ArtistUserInput,
                GenreUserInput = model.GenreUserInput, 
                TrackUserInput  = model.TrackUserInput,
                Tracks = model.Tracks,
                RecommendationsGiven = model.RecommendationsGiven
            });

            return View(seedVerificationModel);
        }

        [HttpPost]
        public async Task<IActionResult> TestVerification(SongRecommendationsIndexModel model)
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
                VerifiedSeeds = seedVerificationModelDto.VerifiedSeeds,
            }; 

            return View(seedVerificationModel);
        }
    }


}
