using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PhysicsServer.Data;
using Darnton.Blazor.DeviceInterop.Geolocation;
using PhysicsServer.Domains.Astronomy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

// Register HTTP Client Factory typed with AstroObjectService data service
var apiSettings = builder.Configuration
    .GetSection(nameof(AstroObjectsAPISettings))
    .Get<AstroObjectsAPISettings>();
builder.Services.AddHttpClient<IAstroObjectService, AstroObjectService>(client =>
{
    client.BaseAddress = new Uri(apiSettings.BaseAddress);
});

// Register geolocation service
builder.Services.AddScoped<IGeolocationService, GeolocationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
