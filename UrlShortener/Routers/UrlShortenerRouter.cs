namespace UrlShortener.Routers;

using FluentValidation;
using UrlShortener.Dtos;

public static class UrlShortenerRouter
{
    public static void RegisterUrlShortenerRoutes(this WebApplication app)
    {
        var urlShortenerGroup = app.MapGroup("/url-shortener").WithTags("Url Shortener");

        urlShortenerGroup.MapPost("/shorten", (ShortenUrlRequestDto request, IValidator<ShortenUrlRequestDto> validator) =>
            {
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var problemDetails = new HttpValidationProblemDetails(validationResult.ToDictionary())
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Validation Error",
                        Detail = "One or more validation errors occurred."
                    };

                    return Results.Problem(problemDetails);
                }

                return Results.Ok($"Shortened URL for {request.Url}");
            })
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