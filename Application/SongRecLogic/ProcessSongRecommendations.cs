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
        private readonly ISeedService _seedService;
        private readonly ISongRecommendationsService _songRecommendationsService;
        public ProcessSongRecommendations(ISearchSpotifyService searchSpotifyService, ISeedService seedService, ISongRecommendationsService songRecsService)
        {
            _searchSpotifyService = searchSpotifyService;
            _seedService = seedService;
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
            var songRecommendationSeeds = _seedService.ProcessSeeds(seedInput); // Splits user input into seed intermediaries with types - ARTIST ONLY RIGHT NOW

            foreach (SeedIntermediary intermediary in songRecommendationSeeds.SeedIntermediaries)
            {
                var artistSearchSummary = await _searchSpotifyService.GetSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType);

                var potentialSeeds = ProcessArtistSearchSummary(artistSearchSummary);

                foreach (PotentialSeed potentialSeed in potentialSeeds)
                {
                    intermediary.PotentialSeeds.Add(potentialSeed);
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

        //public async Task<SongRecSeed> VerifySeedInputsAsync(SongRecSeed seedInput)
        //{
        //    _seedService.ProcessSeeds(seedInput); // Splits user input into seed intermediaries with types - ARTIST ONLY RIGHT NOW

        //    foreach (SeedIntermediary intermediary in seedInput.SeedIntermediaries)
        //    {
        //        var artistSearchSummary = await _searchSpotifyService.GetSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType);

        //        var potentialSeeds = ProcessArtistSearchSummary(artistSearchSummary);

        //        foreach (PotentialSeed potentialSeed in potentialSeeds)
        //        {
        //            intermediary.PotentialSeeds.Add(potentialSeed);
        //        }

        //    }

        //    return seedInput;
        //}
    }
}
