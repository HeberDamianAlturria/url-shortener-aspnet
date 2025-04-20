namespace UrlShortener.Repositories;

using Microsoft.EntityFrameworkCore;
using UrlShortener.Db;
using UrlShortener.Models;

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
