using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using ProjectBlazor.Components;
using ProjectBlazor.Data;
using ProjectBlazor.Components.Pages.Maintenance.Business.Hotels;
using ProjectBlazor.Components.Pages.Maintenance.Business.Types;
using ProjectBlazor.Components.Pages.Maintenance.Business.Locations;
using ProjectBlazor.Components.Pages.Maintenance.Corporate;
using ProjectBlazor.Components.Pages.Maintenance.MembershipPrivilege.Privileges;
using ProjectBlazor.Components.Pages.Maintenance.MembershipPrivilege.Tiers;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "ProjectBlazorAuthCookie";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var apiBaseUrl = Environment.GetEnvironmentVariable("API_BASEURL") ?? throw new InvalidOperationException("API_BASEURL not configured");
var apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? throw new InvalidOperationException("API_KEY not configured");

builder.Services.AddHttpClient<HotelsService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
});

builder.Services.AddHttpClient<TypesService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
});

builder.Services.AddHttpClient<LocationsService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
});

builder.Services.AddHttpClient<CorporateService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
});

builder.Services.AddHttpClient<PrivilegesService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
});

builder.Services.AddHttpClient<TiersService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapGet("/do-logout", async (HttpContext context) =>
{
    await context.SignOutAsync();
    context.Response.Redirect("/login");
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();