using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Appellation.Middleware
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
                
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);

                var userMessage = "All seed values are empty!";
                context.Response.Redirect($"SongRecommendations?message={userMessage}");

            }
            catch (RecommendedTracksNullException ex)
            {
                _logger.LogError(ex, ex.Message);

                var userMessage = $"{ex.Message}";
                context.Response.Redirect($"SongRecommendations?message={userMessage}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                
                context.Response.Redirect("/Error");
            }
        }   
    }
}
