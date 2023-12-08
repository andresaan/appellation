using Newtonsoft.Json;
using Data.Results;
using Application.Interfaces;

namespace Spotify.Services
{
    public class SearchSpotifyService : ISearchSpotifyService
    {
        private IHttpClientFactory _httpClientFactory;

        public SearchSpotifyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task<string> GetSeedSearchResultsAsync(string q, string type)
        {
            var httpClient = _httpClientFactory.CreateClient("Spotify");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/search?q={q}&type={type}&limit=3");

            var response = await httpClient.SendAsync(request);

            //response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content;

        }

        public async Task<ArtistSearchSummary> GetArtistSeedSearchResultsAsync(string q, string type)
        {
            var content = await GetSeedSearchResultsAsync(q, type);

            var artistSearchResult = JsonConvert.DeserializeObject<ArtistSearchResult>(content);

            if (artistSearchResult?.Summary.Artists == null || artistSearchResult.Summary.Artists.Length == 0) {

                //artistSearchResult.Summary.Artists = 
                //    new ArtistComplex[] 
                //    { 
                //        new ArtistComplex() 
                //        { 
                //            Name = "no search results", 
                //            Images = new Image[] { new Image() { Url = "" } } 
                //        } 
                //    };

                artistSearchResult.Summary.NoResults = true;
            }

            return artistSearchResult.Summary;
        }

        public async Task<TrackSearchSummary> GetTrackSeedSearchResultsAsync(string q, string type)
        {
            var content = await GetSeedSearchResultsAsync(q, type);

            var trackSearchResult = JsonConvert.DeserializeObject<TrackSearchResult>(content);

            if (trackSearchResult?.Summary.Tracks == null || trackSearchResult.Summary.Tracks.Length == 0)
            {
                trackSearchResult.Summary.Tracks = 
                    new Track[] 
                    { 
                        new Track() 
                        { 
                            Name = "no search results", 
                            PerformingArtists = new ArtistComplex[] { new ArtistComplex() { Name = "no search results" } }, 
                            Album = new Album() { Images = new Image[] { new Image() { Url = "" } } } 
                        } 
                    };
            }

            return trackSearchResult.Summary;
        }

    }

}
