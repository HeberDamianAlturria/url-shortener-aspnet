namespace UrlShortener.Routers;

using FluentValidation;
using UrlShortener.Dtos;
using UrlShortener.Filters;

public static class UrlShortenerRouter
{
    public static void RegisterUrlShortenerRoutes(this WebApplication app)
    {
        var urlShortenerGroup = app.MapGroup("/url-shortener").WithTags("Url Shortener");

        urlShortenerGroup.MapPost("/shorten", ShortenPost)
            .WithRequestValidation<ShortenUrlRequestDto>()
            .WithName("ShortenUrl")
            .Produces<string>(StatusCodes.Status200OK);

        urlShortenerGroup.MapGet("/redirect", () => "Redirected URL")
            .WithName("RedirectUrl")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        urlShortenerGroup.MapGet("/stats", () => "URL Stats")
            .WithName("GetUrlStats")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static IResult ShortenPost(ShortenUrlRequestDto request) 
    {
        return Results.Ok($"Shortened URL for {request.Url}");
    }
}