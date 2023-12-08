using Application.Interfaces;
using Data.Results;
using Data.Seed;

namespace Application.Handlers
{
    public class SongRecommendationHandler : ISongRecommendationHandler
    {
        private readonly ISearchSpotifyService _searchSpotifyService;
        private readonly ISongRecommendationsService _songRecommendationsService;
        public SongRecommendationHandler(ISearchSpotifyService searchSpotifyService, ISongRecommendationsService songRecsService)
        {
            _searchSpotifyService = searchSpotifyService;
            _songRecommendationsService = songRecsService;
        }
        public async Task<SongRecommendationSeeds> VerifySeedInputsAsync(string? artistInput, string? trackInput, string? genreInput)
        {
            var songRecommendationSeeds = CreateSeedIntermediarys(artistInput, trackInput, genreInput); // Splits user input into seed intermediaries 

            foreach (ArtistSeedIntermediary intermediary in songRecommendationSeeds.ArtistSeedIntermediaries)
            {
                var artistSearchSummary = await _searchSpotifyService.GetArtistSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType); // Search user input

                intermediary.PotentialSeeds = ProcessArtistSearchSummary(artistSearchSummary); // Use result to make potential seeds
            }

            foreach (TrackSeedIntermediary intermediary in songRecommendationSeeds.TrackSeedIntermediaries)
            {
                var trackSearchResult = await _searchSpotifyService.GetTrackSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType); // Search user input

                intermediary.TrackPotentialSeeds = ProcessTrackSearchSummary(trackSearchResult); // Use result to make potential seeds
            }

            return songRecommendationSeeds;
        }

        private SongRecommendationSeeds CreateSeedIntermediarys(string? artistInput, string? trackInput, string? genreInput)
        {
            var songRecommendationSeeds = new SongRecommendationSeeds();

            if (artistInput != null)
            {
                var splitArtistSeeds = artistInput.TrimEnd(',').Split(',').ToList();

                foreach (string artist in splitArtistSeeds)
                {
                    songRecommendationSeeds.ArtistSeedIntermediaries.Add(new ArtistSeedIntermediary()
                    {
                        UserInput = artist,
                        SeedType = "artist"
                    });
                }
            }

            if (trackInput != null)
            {
                var splitTrackSeeds = trackInput.TrimEnd(',').Split(',').ToList();

                foreach (string track in splitTrackSeeds)
                {
                    songRecommendationSeeds.TrackSeedIntermediaries.Add(new TrackSeedIntermediary()
                    {
                        UserInput = track,
                        SeedType = "track"
                    });
                }
            }

            songRecommendationSeeds.GenreVerifiedSeeds = genreInput != null ? genreInput.TrimEnd(',') : "";

            return songRecommendationSeeds;
        }

        private List<ArtistPotentialSeed> ProcessArtistSearchSummary(ArtistSearchSummary searchSummary)
        {
            var potentialSeeds = new List<ArtistPotentialSeed>();

            foreach (ArtistComplex artist in searchSummary.Artists)
            {
                potentialSeeds.Add(new ArtistPotentialSeed()
                {
                    ArtistName = artist.Name,
                    Images = artist.Images,
                    SeedType = artist.Type,
                    SpotifyId = artist.Id

                });
            }

            return potentialSeeds;
        }

        private List<TrackPotentialSeed> ProcessTrackSearchSummary(TrackSearchSummary searchSummary)
        {
            var potentialSeeds = new List<TrackPotentialSeed>();

            foreach (Track track in searchSummary.Tracks)
            {
                potentialSeeds.Add(new TrackPotentialSeed()
                {
                    TrackName = track.Name,
                    PerformingArtist = string.Join(", ", track.PerformingArtists.Select(o => o.Name)),
                    SpotifyId = track.Id,
                    SeedType = track.Type,
                    Images = track.Album.Images
                }); ;
            }

            return potentialSeeds;
        }

        public async Task<Track[]> GetSongRecommendationsAsync(string? artistVerifiedSeeds, string? trackVerifiedSeeds, string? genreVerifiedSeeds, int limit)
        {

            var queryParameters = ConstructQueryParameters(artistVerifiedSeeds, trackVerifiedSeeds, genreVerifiedSeeds, limit);

            var tracks = await _songRecommendationsService.GetSongRecommendationsAsync(queryParameters);

            return tracks;
        }

        private string ConstructQueryParameters(string? artistSeeds, string? trackSeeds, string? genreSeeds, int limit)
        {
            return $"limit={limit}&seed_artists={artistSeeds}&seed_tracks={trackSeeds}&seed_genres={genreSeeds}";
        }

    }
}
