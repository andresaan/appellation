using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Spotify.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
        options.LoginPath = "/user/login";

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

builder.Services.AddHttpClient("Spotify", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.spotify.com/v1");
    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<ISongRecs, SongRecs>();
builder.Services.AddScoped<ISeedService, SeedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
