namespace UrlShortener.Services;

using UrlShortener.Dtos;

public interface IUrlShortenerService
{
    Task<ShortenUrlResponseDto> ShortenUrlAsync(
        ShortenUrlRequestDto request,
        HttpContext httpContext
    );

    Task<string?> GetOriginalUrlAsync(string code);

    Task UpdateVisitCountAsync(string code);
}
