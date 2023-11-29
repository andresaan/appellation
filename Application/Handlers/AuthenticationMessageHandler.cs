using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Net;
using System.ComponentModel;
using Application.Interfaces;

namespace Application.Handlers
{
    public class AuthenticationMessageHandler : DelegatingHandler
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IAuthenticationTokenService _authenticationService;
        public AuthenticationMessageHandler(IHttpContextAccessor httpContextAccessor, IAuthenticationTokenService authenticationService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticationService = authenticationService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                var refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
                var newAccessToken = await _authenticationService.UseRefreshTokenAsync(refreshToken);

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);
                response = await base.SendAsync(request, cancellationToken);
            }

            return response;

            ////ensure success
            //response.EnsureSuccessStatusCode(); 
        }
    }
}
