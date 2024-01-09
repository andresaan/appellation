using Application.Exceptions;
using Application.Interfaces;
using Data.Results;
using Data.Seed;
using Microsoft.Extensions.Options;

namespace Application.Services
{
    public class SongRecommendationProcessingService : ISongRecommendationProcessingService
    {
        private readonly ISearchSpotifyService _searchSpotifyService;
        private readonly ISongRecommendationService _songRecommendationsService;
        public SongRecommendationProcessingService(ISearchSpotifyService searchSpotifyService, ISongRecommendationService songRecommendationService)
        {
            _searchSpotifyService = searchSpotifyService;
            _songRecommendationsService = songRecommendationService;
        }

        public async Task<SongRecommendationSeeds> VerifySeedInputsAsync(string? artistInput, string? trackInput, string? genreInput)
        {
            var songRecommendationSeeds = new SongRecommendationSeeds();
            songRecommendationSeeds.CreateSeedIntermediaries(artistInput, trackInput, genreInput);

            foreach (ArtistSeedIntermediary intermediary in songRecommendationSeeds.ArtistSeedIntermediaries)
            {
                var artistSearchSummary = await _searchSpotifyService.GetArtistSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType); // Search user input
                intermediary.ProcessArtistSearchSummary(artistSearchSummary);
            }

            foreach (TrackSeedIntermediary intermediary in songRecommendationSeeds.TrackSeedIntermediaries)
            {
                var trackSearchResult = await _searchSpotifyService.GetTrackSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType); // Search user input
                intermediary.ProcessTrackSearchSummary(trackSearchResult);
            }

            return songRecommendationSeeds;
        }

        public async Task<Track[]> ProcessSongRecommendationsAsync(string? artistVerifiedSeeds, string? trackVerifiedSeeds, string? genreVerifiedSeeds, int limit, int popularityTarget)
        {
            if (string.IsNullOrEmpty(artistVerifiedSeeds) && string.IsNullOrEmpty(trackVerifiedSeeds) && string.IsNullOrEmpty(genreVerifiedSeeds))
            {
                throw new ArgumentNullException(null, "All seed values are null or empty");
            }
            var queryParameters = ConstructQueryParameters(artistVerifiedSeeds, trackVerifiedSeeds, genreVerifiedSeeds, limit, popularityTarget);
            
            var songRecommendationsResult = await _songRecommendationsService.GetSongRecommendationsAsync(queryParameters);
            
            if (songRecommendationsResult == null || songRecommendationsResult.Tracks == null || songRecommendationsResult.Tracks.Length == 0)
            {
                throw new RecommendedTracksNullException("No recommendations returned");
            }

            return songRecommendationsResult.Tracks;
        }

        private string ConstructQueryParameters(string? artistSeeds, string? trackSeeds, string? genreSeeds, int limit, int popularityTarget)
        {
            var popularityTargetQueryParameter = popularityTarget == -1 ? "" : $"&max_popularity={popularityTarget}";

            return $"limit={limit}&seed_artists={artistSeeds}&seed_tracks={trackSeeds}&seed_genres={genreSeeds}{popularityTargetQueryParameter}";
        }

    }
}
