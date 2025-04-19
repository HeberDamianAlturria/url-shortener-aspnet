namespace UrlShortener.Repositories;

using UrlShortener.Models;
using UrlShortener.Db;
using Microsoft.EntityFrameworkCore;

public class UrlShortenerRepository : IUrlShortenerRepository
{
    private readonly UrlShortenerDbContext _dbContext;

    public UrlShortenerRepository(UrlShortenerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ShortUrlModel> AddAndSaveAsync(ShortUrlModel shortUrlModel)
    {
        await _dbContext.ShortUrls.AddAsync(shortUrlModel);
        await SaveChangesAsync();

        return shortUrlModel;
    }

    public async Task<ShortUrlModel?> GetByCodeAsync(string code)
    {
        return await _dbContext.ShortUrls.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
