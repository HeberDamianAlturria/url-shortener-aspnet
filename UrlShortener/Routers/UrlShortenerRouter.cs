namespace UrlShortener.Routers;

using FluentValidation;
using UrlShortener.Dtos;
using UrlShortener.Filters;
using UrlShortener.Services;

public static class UrlShortenerRouter
{
    public static void RegisterUrlShortenerRoutes(this WebApplication app)
    {
        var urlShortenerGroup = app.MapGroup("/").WithTags("Url Shortener");

        urlShortenerGroup
            .MapPost("/shorten", ShortenPost)
            .WithRequestValidation<ShortenUrlRequestDto>()
            .WithName("ShortenUrl")
            .Produces<string>(StatusCodes.Status200OK);

        urlShortenerGroup
            .MapGet("/{code}", RedirectUrl)
            .WithName("RedirectUrl")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        urlShortenerGroup
            .MapGet("/{code}/stats", (string code) => "URL Stats")
            .WithName("GetUrlStats")
            .Produces<string>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
    }

    public static async Task<IResult> ShortenPost(
        HttpContext httpContext,
        ShortenUrlRequestDto request,
        IUrlShortenerService urlShortenerService
    )
    {
        try
        {
            var result = await urlShortenerService.ShortenUrlAsync(request, httpContext);

            if (result == null)
            {
                return Results.NotFound("URL not found.");
            }

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem(
                "An error occurred while processing your request.",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }

    public static async Task<IResult> RedirectUrl(
        string code,
        IUrlShortenerService urlShortenerService
    )
    {
        try
        {
            var originalUrl = await urlShortenerService.GetOriginalUrlAsync(code);

            if (originalUrl == null)
            {
                return Results.NotFound("URL not found.");
            }

            return Results.Redirect(originalUrl);
        }
        catch (Exception ex)
        {
            return Results.Problem(
                "An error occurred while processing your request.",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}
