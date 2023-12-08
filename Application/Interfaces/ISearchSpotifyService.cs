using Data.Results;

namespace Application.Interfaces
{
    public interface ISearchSpotifyService
    {
        public Task<ArtistSearchSummary> GetArtistSeedSearchResultsAsync(string q, string type);
        public Task<TrackSearchSummary> GetTrackSeedSearchResultsAsync(string q, string type);
    }
}
