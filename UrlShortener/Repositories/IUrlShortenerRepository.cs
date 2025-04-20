namespace UrlShortener.Repositories;

using UrlShortener.Models;

public interface IUrlShortenerRepository
{
    Task<ShortUrlModel> AddAndSaveAsync(ShortUrlModel shortUrlModel);
    Task<ShortUrlModel?> GetByCodeAsync(string code);
    Task SaveChangesAsync();
    Task UpdateVisitCountAsync(ShortUrlModel shortUrlModel);
}
