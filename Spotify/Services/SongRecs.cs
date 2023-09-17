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
using Spotify.Models;

namespace Spotify.Services
{
    public class SongRecs : ISongRecs
    {
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpContextAccessor;
        public  SongRecs(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        //This is going to be a try catch using the seedvalue provided by the users 
        //if the try works and returns an i
        private async Task<bool> CheckSeedValue(string seedValue)
        {
            throw new NotImplementedException();
        }

        //this will not be perfect, add user ability to verify selection and explanation of how to use spotify IDs
        private async Task<string> GetSeedId(string q, string type)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetIdFromSeedValue(string seedValue)
        {
            if ( await CheckSeedValue(seedValue))
            {
                return seedValue;
            }
            else
            {
                return await GetSeedId(seedValue, "seedValue.Type");
            }

        }
        // there can only be max 5 seed values, but at least one
        // genres must be from available seed genre values - use endpoint to validate
        
        
        public async Task<IEnumerable<Track>> GetSongRecsAsync(IEnumerable<string> seedTrack, IEnumerable<string> seedArtist, IEnumerable<string> seedGenre)
        {
            // HttpClient
            var httpClient = _httpClientFactory.CreateClient("Spotify");

            //token
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            //Create new request and set authorization header
            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/recommendations");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            //send request 
            var response = await httpClient.SendAsync(request);

            //ensure success
            response.EnsureSuccessStatusCode();

            //convert response message contents to tracks
            var content = await response.Content.ReadAsStringAsync();
            var tracks = JsonConvert.DeserializeObject<IEnumerable<Track>>(content);

            if (tracks != null)
            {
                return tracks;
            }
            else
                throw new Exception();
        }


    }
}
