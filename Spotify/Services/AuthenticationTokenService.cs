using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Data.Results;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Spotify.Services
{
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string ClientId;
        private readonly string ClientSecret;

        public AuthenticationTokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            ClientId = configuration.GetValue<string>("Spotify:ClientId");
            ClientSecret = configuration.GetValue<string>("Spotify:ClientSecret");
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> UseRefreshTokenAsync(string refreshToken)
        {
            var httpClient = _httpClientFactory.CreateClient("SpotifyAuthentication");

            var request = new HttpRequestMessage(HttpMethod.Post, "/api/token");
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type" , "refresh_token"},
                { "refresh_token", refreshToken },
            });

            var response = await httpClient.SendAsync(request);
            var tokenRefreshResult = JsonConvert.DeserializeObject<TokenRefreshResult>(await response.Content.ReadAsStringAsync());

            if (tokenRefreshResult == null)
            {
                throw new Exception("Error: Unable to parse into AuthResult");
            }

            if (tokenRefreshResult.RefreshToken  == null)
            {
                tokenRefreshResult.RefreshToken = refreshToken;
            }

            StoreNewTokens(tokenRefreshResult);

            return tokenRefreshResult.AccessToken;
        }

        private async void StoreNewTokens(TokenRefreshResult tokenRefreshResult)
        {
            var authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            authenticateResult.Properties.StoreTokens(new List<AuthenticationToken>() {
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = tokenRefreshResult.AccessToken
                },
                new AuthenticationToken()
                {
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = tokenRefreshResult.RefreshToken
                }
            });
        }
    }
}
