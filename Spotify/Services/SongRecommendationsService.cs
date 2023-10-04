using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Application.Interfaces;
using Data.Results;
using Data.Seed;
using Microsoft.IdentityModel.Tokens;

namespace Spotify.Services
{
    public class SongRecommendationsService : ISongRecommendationsService
    {
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpContextAccessor;
        public  SongRecommendationsService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<Track[]> GetSongRecommendationsAsync(string queryParameters)
        {
            // HttpClient
            var httpClient = _httpClientFactory.CreateClient("Spotify");

            //token
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            //Create new request and set authorization header

            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/recommendations?seed_artists={queryParameters}");

            //var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/recommendations?{queryParameters}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            //send request 
            var response = await httpClient.SendAsync(request);

            //ensure success
            //response.EnsureSuccessStatusCode();

            //convert response message contents to tracks
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
