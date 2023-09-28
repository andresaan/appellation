using Application.Interfaces;
using Application.Models.SongRecommendations;
using Data.Results;
using Data.Seed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.SongRecLogic
{
    public class ProcessSongRecommendations : IProcessSongRecommendations
    {
        private readonly ISearchSpotifyService _searchSpotifyService;
        private readonly ISongRecommendationsService _songRecommendationsService;
        public ProcessSongRecommendations(ISearchSpotifyService searchSpotifyService, ISongRecommendationsService songRecsService)
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



        public async Task<SeedVerificationModel> VerifySeedInputsAsync(SongRecommendationsIndexModel seedInput)
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

        public List<PotentialSeed> ProcessArtistSearchSummary(ArtistSearchSummary searchSummary)
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

        public List<TrackPotentialSeed> ProcessTrackSearchSummary(TrackSearchSummary trackSearchSummary)
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

        public async Task<Track[]> GetSongRecommendationsAsync(SeedVerificationModel seedVerifications)
        {
            var verifiedSeeds = SortVerifiedSeeds(seedVerifications);
            
            var songRecommendationSeeds = FilterVerifiedSeedsByType(verifiedSeeds);

            ConstructSeedQueryParameters(songRecommendationSeeds);

            var tracks = await _songRecommendationsService.GetSongRecommendationsAsync(songRecommendationSeeds);

            return tracks;
        }

        public List<VerifiedSeed> SortVerifiedSeeds(SeedVerificationModel seedVerificationModel)
        {
            var verifiedSeedsIntermediary = new List<PotentialSeed>();

            foreach (SeedIntermediary seedIntermediary in seedVerificationModel.SeedIntermediaries)
            {
                
                // not a good solution to seed type value bug - using in order to continue testing 
                foreach (PotentialSeed potentialSeed in seedIntermediary.PotentialSeeds)
                {
                    potentialSeed.SeedType = seedIntermediary.SeedType;
                }

                verifiedSeedsIntermediary.Add(seedIntermediary.PotentialSeeds.First(i => i.Verified == true));
            }

            var verifiedSeeds = new List<VerifiedSeed>();

            foreach (PotentialSeed seed in verifiedSeedsIntermediary)
            {
                verifiedSeeds.Add(new VerifiedSeed()
                {
                    SeedType = seed.SeedType,
                    SpotifyId = seed.SpotifyId
                }); 
            }

            return verifiedSeeds;
        }

        public SongRecommendationSeeds FilterVerifiedSeedsByType(List<VerifiedSeed> verifiedSeeds)
        {
            var songRecommendationSeeds = new SongRecommendationSeeds();

            songRecommendationSeeds.VerifiedArtistSeeds.AddRange(verifiedSeeds.Where(o => o.SeedType == "artist"));

            return songRecommendationSeeds;
        }

        public void ConstructSeedQueryParameters(SongRecommendationSeeds seeds)
        {
            if (seeds.VerifiedArtistSeeds.Any())
            {
                seeds.ArtistSeedQueryParameter = "seed_artists=";

                foreach (VerifiedSeed seed in seeds.VerifiedArtistSeeds)
                {
                    seeds.ArtistSeedQueryParameter = seeds.ArtistSeedQueryParameter + $"{seed.SpotifyId},";
                }

                seeds.ArtistSeedQueryParameter = seeds.ArtistSeedQueryParameter.TrimEnd(',');
            }
        }

        public SeedVerificationModel ProcessSeeds(SongRecommendationsIndexModel seedInput)
        {
            var songRecommendationSeeds = new SeedVerificationModel();

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
    }
}
