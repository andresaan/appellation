using Application.Interfaces;
using Appellation.Models;
using Microsoft.AspNetCore.Mvc;
using Data.Seed;
using Microsoft.AspNetCore.Authorization;
using Data.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections.Generic;

namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {

        private ISongRecommendationHandler _songRecommendationHandler;
        private IFavoritesHandler _favoritesHandler;
        public SongRecommendationsController(ISongRecommendationHandler songRecommendationHandler, IFavoritesHandler favoritesHandler)
        {
            _songRecommendationHandler = songRecommendationHandler;
            _favoritesHandler = favoritesHandler;
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
                Tracks = await _songRecommendationHandler.GetSongRecommendationsAsync(
                    model.ArtistVerifiedSeeds, model.TrackVerifiedSeeds, model.GenreVerifiedSeeds, model.Limit),

                RecommendationsGiven = true
            };

            return View(songRecommendationsIndexModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Verification(SongRecommendationsIndexModel model)
        {
            var songRecommendationSeeds = await _songRecommendationHandler.VerifySeedInputsAsync(model.ArtistUserInput, model.TrackUserInput, model.GenreUserInput);

            var seedVerificationModel = new SeedVerificationModel()
            {
                ArtistSeedIntermediaries = songRecommendationSeeds.SeedIntermediaries,
                TrackSeedIntermediaries = songRecommendationSeeds.TrackSeedIntermediaries,
                GenreVerifiedSeeds = songRecommendationSeeds.GenreVerifiedSeeds,
                Limit = model.Limit
            };

            return View(seedVerificationModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Favorites()
        {
            FavoritesModel favoritesModel = new FavoritesModel();

            favoritesModel.FavoriteTracks = HttpContext.Session.Get<List<Track>>("favorites");

            return View(favoritesModel);
        }

        [HttpPost]
        public void AddFavorite([FromBody] Track track)
        {
            var session = HttpContext.Session;
            var favorites = session.Get<List<Track>>("favorites");

            var favoritesUpdated = _favoritesHandler.addTrackToFavorites(favorites, track);

            session.Set("favorites", favoritesUpdated);
        }

        [HttpPost]
        public void RemoveFavorite([FromBody] Track track)
        {
            var session = HttpContext.Session;
            var favorites = session.Get<List<Track>>("favorites");

            var favoritesUpdated = _favoritesHandler.removeTrackFromFavorites(favorites, track);

            session.Set("favorites", favoritesUpdated);
        }
    }
}

