namespace UrlShortener.Routers;

using FluentValidation;
using UrlShortener.Dtos;
using UrlShortener.Filters;

public static class UrlShortenerRouter
{
    public static void RegisterUrlShortenerRoutes(this WebApplication app)
    {
        var urlShortenerGroup = app.MapGroup("/url-shortener").WithTags("Url Shortener");

        urlShortenerGroup.MapPost("/shorten", (ShortenUrlRequestDto request) =>
            {
                return Results.Ok($"Shortened URL for {request.Url}");
            })
            .WithRequestValidation<ShortenUrlRequestDto>()
            .WithName("ShortenUrl")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest);

        urlShortenerGroup.MapGet("/redirect", () => "Redirected URL")
            .WithName("RedirectUrl")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        urlShortenerGroup.MapGet("/stats", () => "URL Stats")
            .WithName("GetUrlStats")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }
}