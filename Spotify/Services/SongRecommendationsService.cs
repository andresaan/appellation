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
using Application.Handlers;

namespace Spotify.Services
{
    public class SongRecommendationsService : ISongRecommendationsService
    {
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpContextAccessor;
        private AuthenticationMessageHandler _messageHandler;
        public  SongRecommendationsService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, AuthenticationMessageHandler messageHandler)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _messageHandler = messageHandler;
        }
        
        public async Task<Track[]> GetSongRecommendationsAsync(string queryParameters)
        {
            var httpClient = _httpClientFactory.CreateClient("Spotify");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/recommendations?{queryParameters}");
            
            //var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            ////ensure success
            ////response.EnsureSuccessStatusCode(); 

            var response = await httpClient.SendAsync(request);

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
