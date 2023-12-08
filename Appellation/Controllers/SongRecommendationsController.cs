using Application.Interfaces;
using Appellation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Data.Results;

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(SeedVerificationModel model)
        {
            var songRecommendationsIndexModel = new SongRecommendationsIndexModel()
            {
                Tracks = await _songRecommendationHandler.GetSongRecommendationsAsync(
                    model.ArtistVerifiedSeeds, model.TrackVerifiedSeeds, model.GenreVerifiedSeeds, model.Limit, model.PopularityMax)
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
                ArtistSeedIntermediaries = songRecommendationSeeds.ArtistSeedIntermediaries,
                TrackSeedIntermediaries = songRecommendationSeeds.TrackSeedIntermediaries,
                GenreVerifiedSeeds = songRecommendationSeeds.GenreVerifiedSeeds,
                Limit = model.Limit,
                PopularityMax = model.PopularityMax
                
            };

            return View(seedVerificationModel);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Favorites()
        {
            FavoritesModel model = new FavoritesModel() 
            {
                // Need for favorites page .Any() check -- two options 1) logic in cont or 2) checkfavorites()
                FavoriteTracks = _favoritesHandler.CheckFavorites(HttpContext.Session.Get<List<Track>>("favorites"))
            };

            // Old way uses var and then checks if null using if else block - returns new list if null like method
            var favorites = HttpContext.Session.Get<List<Track>>("favorites");
            
            return View(model);
        }

        [HttpPost]
        public void AddFavorite([FromBody] Track track)
        {
            var favorites = HttpContext.Session.Get<List<Track>>("favorites");

            var favoritesUpdated = _favoritesHandler.addTrackToFavorites(favorites, track);

            HttpContext.Session.Set("favorites", _favoritesHandler.addTrackToFavorites(favorites, track));
        }

        [HttpPost]
        public void RemoveFavorite([FromBody] Track track)
        {
            var favorites = HttpContext.Session.Get<List<Track>>("favorites");

            var favoritesUpdated = _favoritesHandler.removeTrackFromFavorites(favorites, track);

            HttpContext.Session.Set("favorites", _favoritesHandler.removeTrackFromFavorites(favorites, track));
        }
    }
}

