using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTracking.Auth;
using TimeTracking.Middleware;
using TimeTracking.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    // Add Authentication to Swagger
    c =>
    {
        c.SwaggerDoc("v1", new() { Title = "TimeTracking API", Version = "v1" });
        c.AddSecurityDefinition("Bearer", new()
        {
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer"
        });
    }
);

builder.Services.AddSingleton<IAuthorizationHandler, EmailDomainHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EmailDomain", policy =>
        policy.Requirements.Add(new EmailDomainRequirement("example.com")));
});

builder.Services.AddAuthentication().AddScheme<ApiKeyOptions, ApiKeyHandler>("APIKEY", o => o.DisplayMessage = "Please provide a valid API key");

builder.Services.AddDbContext<TimeTrackingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("TrackingDbContext")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<TimeTrackingDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// M
app.UseMiddleware<CustomMiddleware>(); // Register the custom middleware

app.Use(async (context, next) =>
{
    if (context == null) throw new ArgumentNullException(nameof(context));
    if (next == null) throw new ArgumentNullException(nameof(next));

    context.Items["CustomData"] = "This is some custom data";
    // Custom logic before the next middleware
    await next();
    // You can access the custom data later in the pipeline
    var customData = context.Items["CustomData"] as string;
    if (customData != null)
    {
        Console.WriteLine($"Custom Data: {customData}");
    }
    Console.WriteLine(context.User.Identity?.Name ?? "No user identity");
});
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapGroup("identity").MapIdentityApi<IdentityUser>();

app.Run();


// https://dev.to/peranv/how-to-implement-oauth-20-authentication-in-aspnet-core-with-external-apis-k7d