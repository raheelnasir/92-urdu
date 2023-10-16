using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Web;
using View.Authentication;
using View.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthenticationCore();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<UserAccountService>();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSingleton<ISignupUser>();
builder.Services.AddHttpClient<IUserProfile, ISignupUser>(
    c => { c.BaseAddress = new Uri("https://localhost:7170/"); }); // Remove '/api/'

builder.Services.AddSingleton<ICreateUser>();
builder.Services.AddHttpClient<IUserProfile, ICreateUser>(
    c => { c.BaseAddress = new Uri("https://localhost:7170/"); }); // Remove '/api/'

// Register the IGetUsersData service
builder.Services.AddHttpClient<IGetUsersData>(
    c => { c.BaseAddress = new Uri("https://localhost:7170/api/"); });


builder.Services.AddHttpClient<IUpdateUsersProfileData>(
    c => { c.BaseAddress = new Uri("https://localhost:7170/"); });

builder.Services.AddSingleton<IDeleteUsersProfile>();

builder.Services.AddHttpClient<IDeleteUsersProfile>(
    c => { c.BaseAddress = new Uri("https://localhost:7170/"); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
