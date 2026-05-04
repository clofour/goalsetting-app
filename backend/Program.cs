using System.Threading.RateLimiting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using backend.Enrichers;
using Serilog.Events;
using Destructurama;
using Microsoft.AspNetCore.Identity;
using backend.Viewmodels;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

string environment = "Testing";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .Destructure.UsingAttributes()
    .Enrich.WithProperty("Name", "Goalsetting")
    .Enrich.WithProperty("Version", "0.1.0")
    .Enrich.WithProperty("Environment", environment)
    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers().WithDestructurers(new[] { new DbUpdateExceptionDestructurer() }))
    .Enrich.With<RequestEnricher>()

    .WriteTo.Console(outputTemplate: "{Name} {Version} ({Environment}) [{Timestamp:HH:mm:ss} {Level:u3}] {RequestData} {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSerilog();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql($"Host=localhost:5432;Username={environment.ToLower()};Password={(environment == "Production" ? File.ReadAllText("/run/secrets/p_db_user_password") : "testing")};Database={environment.ToLower()}")
    .UseSeeding((context, _) =>
    {
        AppDbContext appDbContext = (AppDbContext)context;
        if (appDbContext.AccessCodes.Find("cat") == null)
        {
            AccessCode newAccessCode = new AccessCode("cat", 10);
            appDbContext.AccessCodes.Add(newAccessCode);

            context.SaveChanges();
        }
    });
});

builder.Services.AddDataProtection();

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 16;
    options.Password.RequiredUniqueChars = 0;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    options.User.RequireUniqueEmail = true;

    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(1);

})
.AddSignInManager<SignInManager<User>>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "__Host-AuthToken";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.SlidingExpiration = false;

    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };
});

builder.Services.AddAuthentication().AddCookie("Identity.Application");

builder.Services.AddAuthorization();

builder.Services.AddRateLimiter(options =>
{
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");

        Log.Warning("Ratelimit: {username}", context.HttpContext.User.Identity?.Name ?? "anonymous");
    };

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ?? "anonymous",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 30,
                Window = TimeSpan.FromMinutes(1)
            }));
});

builder.Services.AddOpenApi();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<NorthStarCreate, NorthStar>();
    cfg.CreateMap<BearingCreate, Bearing>();
    cfg.CreateMap<MovementCreate, Movement>();

    cfg.CreateMap<NorthStar, NorthStarGet>();
    cfg.CreateMap<Bearing, BearingGet>();
    cfg.CreateMap<Movement, MovementGet>();
});

builder.Services.AddScoped<GoalService>();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = "{RequestUserAgent} {RequestMethod} {RequestPath} ..{Elapsed}ms.. {StatusCode}";

    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);
        diagnosticContext.Set("RequestScheme", httpContext.Request.Scheme);
        diagnosticContext.Set("RequestUserAgent", httpContext.Request.Headers.UserAgent);
    };
});

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action}"
);

app.Run();
