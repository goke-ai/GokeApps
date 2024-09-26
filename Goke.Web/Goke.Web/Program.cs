using Goke.Web;
using Goke.Web.Client.Pages;
using Goke.Web.Client.Services;
using Goke.Web.Components;
using Goke.Web.Components.Account;
using Goke.Web.Data;
using Goke.Web.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;


bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
bool isLinux = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
bool isOSX = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

//
DatabaseType? databaseType = DatabaseType.MSSQL;
if (isWindows)
{
    databaseType = DatabaseType.MSSQL;
}
if (isLinux)
{
    databaseType = DatabaseType.MySQL;
}

var connectionString = string.Empty;
switch (databaseType)
{
    case DatabaseType.MSSQL:
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>        
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Goke.Web.WindowsMigrations")));
        break;

    case DatabaseType.MySQL:
        connectionString = builder.Configuration.GetConnectionString("MariaDBDefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var serverVersion = new MariaDbServerVersion(new Version(8, 0, 2));

        //builder.Services.AddDbContext<ApplicationDbContext>(options =>
        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly("Goke.Web.LinuxMigrations")));

        break;
    case DatabaseType.Sqlite:
        //builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        //    options.UseSqlite($"Data Source={nameof(Goke.Web)}.db"));
        break;
    default:
        break;
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
builder.Services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();

builder.Services.AddQuickGridEntityFrameworkAdapter();

builder.Services.AddHttpClient();
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration["FrontendUrl"] ?? "https://localhost:7221")
    });

builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddScoped<State>( sp =>
    {
        var x = new State();
        x["Name"] = "Olugbolade Oladokun";
        return x;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerFactory>()
    .CreateLogger<Program>();
logger.LogInformation("Logged after the app is built in the Program file.");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
};

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Goke.Web.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapGet("/api/WeatherForecast", () =>
{
    var startDate = DateOnly.FromDateTime(DateTime.Now);
    var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
    var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = startDate.AddDays(index),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = summaries[Random.Shared.Next(summaries.Length)]
    }).ToArray();

    return forecasts;
})
//.RequireAuthorization()
.WithName("GetWeatherForecast")
.WithOpenApi();

// Initialize Database
await DataInitializer.Initialize(app);

app.Run();

