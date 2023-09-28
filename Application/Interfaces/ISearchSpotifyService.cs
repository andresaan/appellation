using Data.Results;
using Data.Seed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchSpotifyService
    {
        //rivate Task<string> GetSeedSearchResultsAsync(string q, string type);
        public Task<ArtistSearchSummary> GetArtistSeedSearchResultsAsync(string q, string type);
        public Task<TrackSearchSummary> GetTrackSeedSearchResultsAsync(string q, string type);

    }
}
