using Newtonsoft.Json;
using Data.Results;
using Application.Interfaces;

namespace Spotify.Services
{
    public class SearchSpotifyService : ISearchSpotifyService
    {
        private HttpClient _httpClient;
        public SearchSpotifyService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Spotify");
        }
        private async Task<string> GetSeedSearchResultsAsync(string q, string type)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_httpClient.BaseAddress}/search?q={q}&type={type}&limit=3");

            var response = await _httpClient.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            
            return content;
        }
        public async Task<ArtistSearchSummary> GetArtistSeedSearchResultsAsync(string q, string type)
        {
            var content = await GetSeedSearchResultsAsync(q, type);
            var artistSearchResult = JsonConvert.DeserializeObject<ArtistSearchResult>(content);
            
            var artistSearchSummary = artistSearchResult != null ? artistSearchResult.Summary : new ArtistSearchSummary();

            if (artistSearchSummary.Artists == null || artistSearchSummary.Artists.Length == 0)
            {
                artistSearchSummary.NoResults = true;
            }

            return artistSearchSummary;
        }
        public async Task<TrackSearchSummary> GetTrackSeedSearchResultsAsync(string q, string type)
        {
            var content = await GetSeedSearchResultsAsync(q, type);
            var trackSearchResult = JsonConvert.DeserializeObject<TrackSearchResult>(content);

            var trackSearchSummary = trackSearchResult != null ? trackSearchResult.Summary : new TrackSearchSummary();

            if (trackSearchSummary.Tracks == null || trackSearchSummary.Tracks.Length == 0)
            {
                trackSearchSummary.NoResults = true;
            }

            return trackSearchSummary;
        }
    }
}
