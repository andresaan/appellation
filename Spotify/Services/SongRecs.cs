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

namespace Spotify.Services
{
    public class SongRecs
    {
        private IHttpClientFactory _httpClientFactory;
        private IHttpContextAccessor _httpContextAccessor;
        public  SongRecs(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task GetSongRecsAsync()
        {
            // HttpClient
            var httpClient = _httpClientFactory.CreateClient("Spotify");

            //token
            var token = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            //Create new request
            var request = new HttpRequestMessage(HttpMethod.Get, $"{httpClient.BaseAddress}/recommendations");
            // set authorization header
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await httpClient.SendAsync(request);
            //deserialize response
            
            //return mapped response


            throw new NotImplementedException();
        }


    }
}
