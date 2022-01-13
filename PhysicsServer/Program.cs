using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PhysicsServer.Data;
using PhysicsServer.Domains.Astronomy;
using Blazorise.Bootstrap;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Humanizer.Inflections;

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


// Register Blazosire service
builder.Services
    .AddBlazorise(options =>
    {
        options.ChangeTextOnKeyPress = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();
    

var app = builder.Build();
app.Logger.LogInformation($"\n\n\n" +
    $"API Address: {apiSettings.BaseAddress}" +
    $"\n\n\n");

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

// Set physics jargon (used in the Humanizer program)
Vocabularies.Default.AddPlural("Nebula", "Nebulae");

app.Run();
