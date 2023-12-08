using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Application.Interfaces;
using Data.Results;
using Application.Handlers;

namespace Spotify.Services
{
    public class SongRecommendationsService : ISongRecommendationsService
    {
        private IHttpClientFactory _httpClientFactory;
        public  SongRecommendationsService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<Track[]> GetSongRecommendationsAsync(string queryParameters)
        {
            var httpClient = _httpClientFactory.CreateClient("Spotify");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/recommendations?{queryParameters}");

            var response = await httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();
            var songRecommendationsResult = JsonConvert.DeserializeObject<SongRecommendationsResult>(content);
            
            if (songRecommendationsResult != null)
            {
                var tracks = songRecommendationsResult.Tracks;
                return tracks;
            }
            else
                throw new Exception();
        }

    }
}
