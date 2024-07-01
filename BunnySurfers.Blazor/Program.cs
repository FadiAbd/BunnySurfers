using BunnySurfers.API.Entities;
using BunnySurfers.Blazor.Components;
using BunnySurfers.Blazor.Components.Account;
using BunnySurfers.Blazor.Data;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddSingleton<IReturnUrlService, ReturnUrlService>();

// HttpClient för att kommunicera med API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetValue<string>("APIRootUrl") ?? throw new Exception("APIRootUrl is missing from appsettings.json")),
    Timeout = TimeSpan.FromMinutes(4)
});

// Konfigurera databasen
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Konfigurera Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();

// Använd en no-op email-sändare under utveckling
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Choose whether or not to seed/check the app database
var seedAppData = builder.Configuration.GetValue<bool>("SeedAppData");
var resetAppData = builder.Configuration.GetValue<bool>("ResetAppData");
if (seedAppData)
    builder.Services.AddTransient<SeedUserData>();

var app = builder.Build();

// Konfigurera HTTP-request pipelinen
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Seed the database
if (seedAppData)
{
    using var scope = app.Services.CreateScope();
    var seedService = scope.ServiceProvider.GetRequiredService<SeedUserData>();
    if (resetAppData)
        seedService.ClearUserDatabase();
    await seedService.SeedUserRoles();
    await seedService.SeedUserDatabase();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Kör Blazor-komponenter
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Lägg till Identity /Account Razor-komponenter
app.MapAdditionalIdentityEndpoints();

app.Run();
