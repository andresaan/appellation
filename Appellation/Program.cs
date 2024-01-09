using Microsoft.AspNetCore.Authentication.Cookies;
using Spotify.Services;
using Application.Interfaces;
using Application.Handlers;
using Application.Services;
using Appellation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

builder.Services.AddLogging(configure =>
{
    configure.AddConsole();
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Spotify", policy =>
    {
        policy.AuthenticationSchemes.Add("Spotify");
        policy.RequireAuthenticatedUser();

    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(59);
        options.LoginPath = "/home/login";

    })
    .AddSpotify(options =>
    {

        var scopes = new List<string>()
        {
            "user-top-read",
            "user-read-email",
            "user-read-private",
            "playlist-read-private"
        };

        options.ClientId = builder.Configuration["Spotify:ClientId"];
        options.ClientSecret = builder.Configuration["Spotify:ClientSecret"];
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.Scope.Add(String.Join(",", scopes));
        options.SaveTokens = true;

    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthenticationMessageHandler>();
builder.Services.AddHttpClient("Spotify", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.spotify.com/v1");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
})
    .AddHttpMessageHandler<AuthenticationMessageHandler>();

builder.Services.AddHttpClient("SpotifyAuthentication", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://accounts.spotify.com");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<ISongRecommendationService, Spotify.Services.SongRecommendationService>();
builder.Services.AddScoped<ISearchSpotifyService, SearchSpotifyService>();
builder.Services.AddScoped<ISongRecommendationProcessingService, Application.Services.SongRecommendationProcessingService>();
builder.Services.AddScoped<IFavoritesService, FavoritesService>();
builder.Services.AddScoped<ITokenAuthenticationService, TokenAuthenticationService>();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    //app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "songrecommendations",
    pattern: "songrecommendations/{action}/{id?}",
    defaults: new { controller = "SongRecommendations" });

app.Run();
