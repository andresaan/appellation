using Application.Dtos;
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

        public async Task<SeedVerificationDto> VerifySeedInputsAsync(SongRecommendationsDto seedInput)
        {
            var songRecommendationSeeds = ProcessSeeds(seedInput); // Splits user input into seed intermediaries with types - MISSING GENRE

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

        private SeedVerificationDto ProcessSeeds(SongRecommendationsDto seedInput)
        {
            var songRecommendationSeeds = new SeedVerificationDto();

            if (seedInput.ArtistUserInput != null)
            {
                var splitArtistSeeds = seedInput.ArtistUserInput.Split(',').ToList();

                foreach (string artist in splitArtistSeeds)
                {
                    songRecommendationSeeds.SeedIntermediaries.Add(new SeedIntermediary()
                    {
                        UserInput = artist,
                        SeedType = "artist"
                    });
                }
            }

            if (seedInput.TrackUserInput != null)
            {
                var splitTrackSeeds = seedInput.TrackUserInput.Split(',').ToList();

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


        public async Task<Track[]> GetSongRecommendationsAsync(SeedVerificationDto seedVerifications)
        {

            var songRecommendationSeeds = new SongRecommendationSeeds()
            {
                ArtistVerifiedSeeds = seedVerifications.ArtistVerifiedSeeds,
                TrackVerifiedSeeds = seedVerifications.TrackVerifiedSeeds,

                TArtistVerifiedSeeds = seedVerifications.TArtistVerifiedSeeds
            };

            //var queryParameters = ConstructQueryParameters(songRecommendationSeeds);
            //var tracks = await _songRecommendationsService.GetSongRecommendationsAsync(queryParameters);
            
            var tQueryParameters = string.Join(',', songRecommendationSeeds.TArtistVerifiedSeeds);
            
            var tracks = await _songRecommendationsService.GetSongRecommendationsAsync(tQueryParameters);


            return tracks;
        }

        private string ConstructQueryParameters(SongRecommendationSeeds seeds)
        {
            // no longer need verified seed typing which means verified seed obj isn't necessary and should be a list<string>
            // this will change the need for foreach loops and instead joins should be used
            
            if (seeds.ArtistVerifiedSeeds.Any())
            {

                foreach (VerifiedSeed seed in seeds.ArtistVerifiedSeeds)
                {
                    seeds.ArtistSeedQueryParameter = seeds.ArtistSeedQueryParameter + $"{seed.SpotifyId},";
                }

                seeds.ArtistSeedQueryParameter = seeds.ArtistSeedQueryParameter.TrimEnd(',');
            }

            if (seeds.TrackVerifiedSeeds.Any())
            {
                
                foreach (VerifiedSeed seed in seeds.TrackVerifiedSeeds)
                {
                    seeds.TrackSeedQueryParameter = seeds.TrackSeedQueryParameter + $"{seed.SpotifyId},";
                }

                seeds.TrackSeedQueryParameter.TrimEnd(',');
            }

            var queryParameters = $"seed_artists={seeds.ArtistSeedQueryParameter}&seed_tracks={seeds.TrackSeedQueryParameter}";

            return queryParameters;
        }

    }
}
