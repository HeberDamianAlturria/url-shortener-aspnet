namespace UrlShortener.Routers;

using FluentValidation;
using UrlShortener.Dtos;
using UrlShortener.Exceptions;
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
        IUrlShortenerService urlShortenerService,
        ILogger<ShortenUrlRequestDto> logger
    )
    {
        try
        {
            var result = await urlShortenerService.ShortenUrlAsync(request, httpContext);

            if (result == null)
            {
                return Results.NotFound(
                    new ErrorResponseDto(
                        Message: "URL not found",
                        Details: "The URL could not be shortened."
                    )
                );
            }

            return Results.Ok(result);
        }
        catch (DuplicateCodeException ex)
        {
            logger.LogError(ex, "Duplicate code error occurred.");
            return Results.Problem(ex.Message, statusCode: StatusCodes.Status409Conflict);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while shortening the URL.");
            return Results.Problem(
                "An error occurred while processing your request.",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }

    public static async Task<IResult> RedirectUrl(
        string code,
        IUrlShortenerService urlShortenerService,
        ILogger<string> logger
    )
    {
        try
        {
            var originalUrl = await urlShortenerService.GetOriginalUrlAsync(code);

            if (originalUrl == null)
            {
                logger.LogWarning("URL not found for code: {Code}", code);
                return Results.NotFound(
                    new ErrorResponseDto(
                        Message: "URL not found",
                        Details: $"URL not found for code: {code}."
                    )
                );
            }

            return Results.Redirect(originalUrl);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while redirecting the URL.");
            return Results.Problem(
                "An error occurred while processing your request.",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }
    }
}
