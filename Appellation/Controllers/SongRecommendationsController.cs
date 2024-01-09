using Application.Interfaces;
using Appellation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Data.Results;
using Appellation.Extensions;


namespace Appellation.Controllers
{
    public class SongRecommendationsController : Controller
    {

        private ISongRecommendationProcessingService _songRecommendationService;
        private IFavoritesService _favoritesService;
        public SongRecommendationsController(ISongRecommendationProcessingService songRecommendationHandler, IFavoritesService favoritesHandler)
        {
            _songRecommendationService = songRecommendationHandler;
            _favoritesService = favoritesHandler;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index(string? message)
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
                Tracks = await _songRecommendationService.ProcessSongRecommendationsAsync(
                    model.ArtistVerifiedSeeds, model.TrackVerifiedSeeds, model.GenreVerifiedSeeds, model.Limit, model.PopularityMax)
            };

            return View(songRecommendationsIndexModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Verification(SongRecommendationsIndexModel model)
        {
            var songRecommendationSeeds =
                await _songRecommendationService.VerifySeedInputsAsync(model.ArtistUserInput, model.TrackUserInput, model.GenreUserInput);
                
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
            var favorites = HttpContext.Session.Get<List<Track>>("Favorites");

            FavoritesModel model = new FavoritesModel() 
            {
                FavoriteTracks = favorites ?? new List<Track>()
            };

            return View(model);
        }

        [HttpPost]
        public void AddFavorite([FromBody] Track track)
        {
            var favorites = HttpContext.Session.Get<List<Track>>("Favorites");

            HttpContext.Session.Set("Favorites", _favoritesService.addTrackToFavorites(favorites, track));
        }

        [HttpPost]
        public void RemoveFavorite([FromBody] Track track)
        {
            var favorites = HttpContext.Session.Get<List<Track>>("Favorites");

            HttpContext.Session.Set("Favorites", _favoritesService.removeTrackFromFavorites(favorites, track));
        }

    }
}

