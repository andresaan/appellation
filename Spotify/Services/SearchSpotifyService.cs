using Data.Seed;
using Newtonsoft.Json;
using Data.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Application.Interfaces;
using System.Runtime.Serialization;

namespace Spotify.Services
{
    public class SearchSpotifyService : ISearchSpotifyService
    {
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpContextAccessor;

        public SearchSpotifyService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<string> GetSeedSearchResultsAsync(string q, string type)
        {
            // making artist search - returning search result
            var httpClient = _httpClientFactory.CreateClient("Spotify");
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/search?q={q}&type={type}&limit=3");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.SendAsync(request);

            //response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            return content;

        }

        public async Task<ArtistSearchSummary> GetArtistSeedSearchResultsAsync(string q, string type)
        {
            var content = await GetSeedSearchResultsAsync(q, type);

            var artistSearchResult = JsonConvert.DeserializeObject<ArtistSearchResult>(content);
            return artistSearchResult.Summary;
        }

        public async Task<TrackSearchSummary> GetTrackSeedSearchResultsAsync(string q, string type)
        {
            var content = await GetSeedSearchResultsAsync(q, type);

            var trackSearchResult = JsonConvert.DeserializeObject<TrackSearchResult>(content);
            return trackSearchResult.TrackSearchSummary;
        }

    }

}
