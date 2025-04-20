namespace UrlShortener.Services;

using UrlShortener.Dtos;
using UrlShortener.Models;
using UrlShortener.Repositories;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly IUrlShortenerRepository _urlShortenerRepository;

    public UrlShortenerService(IUrlShortenerRepository urlShortenerRepository)
    {
        _urlShortenerRepository = urlShortenerRepository;
    }

    private string GenerateCode()
    {
        return Guid.NewGuid().ToString("N").Substring(0, 10);
    }

    public async Task<ShortenUrlResponseDto> ShortenUrlAsync(ShortenUrlRequestDto request, HttpContext httpContext)
    {
        var currentUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        var code = GenerateCode();
        var shortenedUrl = $"{currentUrl}/{code}";


        var shortUrlModel = new ShortUrlModel
        {
            OriginalUrl = request.Url,
            ShortenedUrl = shortenedUrl,
            Code = code,
        };

        await _urlShortenerRepository.AddAndSaveAsync(shortUrlModel);

        return new ShortenUrlResponseDto(shortUrlModel.ShortenedUrl, shortUrlModel.OriginalUrl);
    }

    public async Task<string?> GetOriginalUrlAsync(string code)
    {
        var shortUrlModel = await _urlShortenerRepository.GetByCodeAsync(code);
        return shortUrlModel?.OriginalUrl;
    }

}