
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
        //General Flow: User input* - process seeds* - validate user input* - get song recs

        //Validation Flow: processed seeds* - search endpoint* - display possibilities to user* - have user confirm seeds*
        //Search Flow: make a search for each seed value and return potential seeds*

        //Song Rec Flow: verified seeds sorted and types - search - response - display


        //Organization/Refactor: Follow above flows and ensure separation of concerns. For data, use the classes created where needed. 
        //Include remaining 2 types in flows

        public async Task<SongRecommendationSeeds> VerifySeedInputsAsync(string? artistInput, string? trackInput, string? genreInput)
        {
            var songRecommendationSeeds = ProcessSeeds(artistInput, trackInput, genreInput); // Splits user input into seed intermediaries with types - MISSING GENRE

            foreach (SeedIntermediary intermediary in songRecommendationSeeds.SeedIntermediaries)
            {
                var artistSearchSummary = await _searchSpotifyService.GetArtistSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType);

                var potentialSeeds = ProcessArtistSearchSummary(artistSearchSummary);

                foreach (PotentialSeed potentialSeed in potentialSeeds)
                {
                    intermediary.PotentialSeeds.Add(potentialSeed);
                }

            }

            foreach (TrackSeedIntermediary intermediary in songRecommendationSeeds.TrackSeedIntermediaries)
            {
                var trackSearchResult = await _searchSpotifyService.GetTrackSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType);

                var potentialSeeds = ProcessTrackSearchSummary(trackSearchResult);

                foreach (TrackPotentialSeed potentialSeed in potentialSeeds)
                {
                    intermediary.TrackPotentialSeeds.Add(potentialSeed);
                }

            }

            return songRecommendationSeeds;
        }

        private SongRecommendationSeeds ProcessSeeds(string? artistInput, string? trackInput, string? genreInput)
        {
            var songRecommendationSeeds = new SongRecommendationSeeds();

            if (artistInput != null)
            {
                var splitArtistSeeds = artistInput.Split(',').ToList();

                foreach (string artist in splitArtistSeeds)
                {
                    songRecommendationSeeds.SeedIntermediaries.Add(new SeedIntermediary()
                    {
                        UserInput = artist,
                        SeedType = "artist"
                    });
                }
            }

            if (trackInput != null)
            {
                var splitTrackSeeds = trackInput.Split(',').ToList();

                foreach (string track in splitTrackSeeds)
                {
                    songRecommendationSeeds.TrackSeedIntermediaries.Add(new TrackSeedIntermediary()
                    {
                        UserInput = track,
                        SeedType = "track"
                    });
                }
            }

            return songRecommendationSeeds;
        }

        private List<PotentialSeed> ProcessArtistSearchSummary(ArtistSearchSummary searchSummary)
        {
            var potentialSeeds = new List<PotentialSeed>();

            foreach (ArtistComplex artist in searchSummary.Artists)
            {
                potentialSeeds.Add(new PotentialSeed()
                {
                    ArtistName = artist.Name,
                    Images = artist.Images,
                    SeedType = artist.Type,
                    SpotifyId = artist.Id

                });
            }

            return potentialSeeds;
        }

        private List<TrackPotentialSeed> ProcessTrackSearchSummary(TrackSearchSummary trackSearchSummary)
        {
            var potentialSeeds = new List<TrackPotentialSeed>();

            foreach (Track track in trackSearchSummary.Tracks)
            {
                potentialSeeds.Add(new TrackPotentialSeed()
                {
                    TrackName = track.Name,
                    PerformingArtist = track.PerformingArtists[0].Name,
                    SpotifyId = track.Id,
                    SeedType = track.Type,
                    Images = track.Album.Images

                }); ;
            }

            return potentialSeeds;
        }

        public async Task<Track[]> GetSongRecommendationsAsync(string[]? artistVerifiedSeeds, string[]? trackVerifiedSeeds, string[]? genreVerifiedSeeds)
        {

            var queryParameters = ConstructQueryParameters(artistVerifiedSeeds, trackVerifiedSeeds, genreVerifiedSeeds);

            var tracks = await _songRecommendationsService.GetSongRecommendationsAsync(queryParameters);


            return tracks;
        }

        private string ConstructQueryParameters(string[]? artistSeeds, string[]? trackSeeds, string[]? genreSeeds)
        {
            var ArtistSeedQueryParameters = artistSeeds != null ? string.Join(',', artistSeeds) : "";

            var TrackSeedQueryParameters = trackSeeds != null ? string.Join(',', trackSeeds) : "";

            var queryParameters = $"seed_artists={ArtistSeedQueryParameters}&seed_tracks={TrackSeedQueryParameters}";

            return queryParameters;
        }

    }
}
