using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Application.Interfaces;
using Data.Results;
using Application.Handlers;
using Application.Exceptions;

namespace Spotify.Services
{
    public class SongRecommendationService : ISongRecommendationService
    {
        private HttpClient _httpClient;
        public  SongRecommendationService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Spotify");
        }

        public async Task<SongRecommendationsResult> GetSongRecommendationsAsync(string queryParameters)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_httpClient.BaseAddress}/recommendations?{queryParameters}");

            var response = await _httpClient.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            var songRecommendationsResult = JsonConvert.DeserializeObject<SongRecommendationsResult>(content);

            return songRecommendationsResult != null ? songRecommendationsResult : new SongRecommendationsResult();
        }

    }
}
