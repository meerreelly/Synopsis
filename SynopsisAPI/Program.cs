using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Core;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Repository.Data_Services;
using Repository.Domain;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


//load env file
Env.Load("../.env");


//logs
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Вивід логів у консоль
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) // Запис у файл із ротацією
    .Enrich.FromLogContext() // Додаткова інформація про контекст
    .MinimumLevel.Information() // Мінімальний рівень логів
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();



string jwtKey = Env.GetString("JWT_KEY");
string googleClientId = Env.GetString("GOOGLE_CLIENT_ID");
string googleClientSecret = Env.GetString("GOOGLE_CLIENT_SECRET");
string discordClientId = Env.GetString("DISCORD_CLIENT_ID");
string discordClientSecret = Env.GetString("DISCORD_CLIENT_SECRET");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception is SecurityTokenExpiredException)
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }

                return Task.CompletedTask;
            }
        };
    }).AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = googleClientId;
        options.ClientSecret = googleClientSecret;
        options.CallbackPath = "/google/signin";
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.SaveTokens = true;
        options.ClaimActions.MapJsonKey("picture", "picture");
    }).AddDiscord(options =>
    {
        options.ClientId = discordClientId;
        options.ClientSecret = discordClientSecret;
        options.CallbackPath = new PathString("/discord/signin");
        options.Scope.Add("identify");
        options.Scope.Add("email");

    }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/discord/login";
    });





builder.Services.AddScoped<Context>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Starting application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush(); 
}