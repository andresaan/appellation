using Application.Interfaces;
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
    public class SongRecLogic : ISongRecommendationLogic
    {
        private readonly ISearchSpotifyService _searchSpotifyService;
        private readonly ISeedService _seedService;
        private readonly ISongRecsService _songRecsService;
        public SongRecLogic(ISearchSpotifyService searchSpotifyService, ISeedService seedService, ISongRecsService songRecsService)
        {
            _searchSpotifyService = searchSpotifyService;
            _seedService = seedService;
            _songRecsService = songRecsService;
        }
        //General Flow: User input* - process seeds* - validate user input* - get song recs
        //Validation Flow: processed seeds* - search endpoint* - display possibilities to user* - have user confirm seeds* - use verified seeds
        //Search Flow: make a search for each seed value and return potential seeds*
        //Song Rec Flow:
        //Organization/Refactor: Follow above flows and ensure separation of concerns. For data, use the classes created where needed. 
        //Include remaining 2 types in flows

        public async Task<SongRecSeed> VerifySeedInputsAsync(SongRecSeed seedInput)
        {
            _seedService.ProcessSeeds(seedInput); // Splits user input into seed intermediaries with types - ARTIST ONLY RIGHT NOW

            foreach (SeedIntermediary intermediary in seedInput.SeedIntermediaries)
            {
                var artistSearchSummary = await _searchSpotifyService.GetSeedSearchResultsAsync(intermediary.UserInput, intermediary.SeedType);

                var potentialSeeds = ProcessArtistSearchSummary(artistSearchSummary);

                foreach (PotentialSeed potentialSeed in potentialSeeds)
                {
                    intermediary.PotentialSeeds.Add(potentialSeed);
                }

            }

            return seedInput;
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
                    SpotifyId = artist.Id
                });
            }

            return potentialSeeds;
        }

    }
}
