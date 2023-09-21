using Data.Results;
using Data.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISearchSpotifyService
    {
        public Task<ArtistSearchSummary?> GetSeedSearchResultsAsync(string q, string type);
    }
}
