using UrlShortener.Routers;
using UrlShortener.Db;

using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to allow all origins, methods, and headers.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add swagger services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FluentValidation services. This will automatically register all validators in the assembly.
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add DbContext for URL shortener.
builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
    options.UseInMemoryDatabase("UrlShortenerDb"));

var app = builder.Build();

// Use CORS policy.
app.UseCors("AllowAll");

// Use Swagger middleware.
app.UseSwagger();
app.UseSwaggerUI();

// Register the URL shortener routes.
app.RegisterUrlShortenerRoutes();

app.MapGet("/", () => "Hello World!");

app.Run();
