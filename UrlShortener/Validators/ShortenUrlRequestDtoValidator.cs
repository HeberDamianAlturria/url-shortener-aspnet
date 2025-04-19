namespace UrlShortener.Validators;

using FluentValidation;
using UrlShortener.Dtos;

public class ShortenUrlRequestDtoValidator : AbstractValidator<ShortenUrlRequestDto>
{
    public ShortenUrlRequestDtoValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("URL cannot be empty.")
            .Must(url =>
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uriResult))
                    return false;

                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            })
            .WithMessage("Invalid URL format. Please provide a valid absolute URL with http or https scheme.");
    }
}
