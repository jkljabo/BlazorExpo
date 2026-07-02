using BlazorCodeChallenge;
using BlazorCodeChallenge.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddScoped<FooterBrandService>();
//builder.Services.AddScoped<ThemeService>();

builder.Services.AddScoped<AppState>();

//builder.Services.AddScoped(sp => new HttpClient());

await builder.Build().RunAsync();