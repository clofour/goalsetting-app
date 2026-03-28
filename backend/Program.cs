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

var builder = WebApplication.CreateBuilder(args);

string environment = "Testing";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .Destructure.UsingAttributes()
    .Enrich.WithProperty("Name", "Messaging")
    .Enrich.WithProperty("Version", "1.0.0")
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
    options.UseNpgsql($"Host=/var/run/postgresql;Username={environment.ToLower()};Password={(environment == "Production" ? File.ReadAllText("/run/secrets/p_db_user_password") : "testing")};Database={environment.ToLower()}")
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

}).AddEntityFrameworkStores<AppDbContext>();

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

builder.Services.AddAuthentication();

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
                PermitLimit = 15,
                Window = TimeSpan.FromMinutes(1)
            }));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action}"
);

app.Run();
