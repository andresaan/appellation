using Data.Seed;
using Newtonsoft.Json;
using Spotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Data.Results;

namespace Spotify.Services
{
    public class SearchSpotifyService
    {
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpContextAccessor;

        public SearchSpotifyService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SeedInput> ArtistSeedSearchResultsAsync(SeedInput seedInput)
        {
           
            var httpClient = _httpClientFactory.CreateClient("Spotify");
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/search");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.SendAsync(request);
            
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var artistSearchSummary = JsonConvert.DeserializeObject<ArtistSearchSummary>(content);

            foreach (Artist artist in artistSearchSummary.Artists)
            {

            }

            if (potentialArtistSeeds != null)
            {
                return seedInput;
            }
            else
                throw new Exception();
            
        }
    }
}
