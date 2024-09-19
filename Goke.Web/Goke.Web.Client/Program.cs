using Goke.Web.Client;
using Goke.Web.Client.Services;
using Goke.Web.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddHttpClient("Goke.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Goke.Web.ServerAPI"));

//builder.Services.AddScoped(sp =>
//    new HttpClient
//    {
//        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7221")
//    });

builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddScoped<State>();

await builder.Build().RunAsync();
