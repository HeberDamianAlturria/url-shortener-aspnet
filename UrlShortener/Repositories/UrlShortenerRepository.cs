namespace UrlShortener.Repositories;

using Microsoft.EntityFrameworkCore;
using Npgsql;
using UrlShortener.Db;
using UrlShortener.Exceptions;
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
        try
        {
            await _dbContext.ShortUrls.AddAsync(shortUrlModel);
            await SaveChangesAsync();

            return shortUrlModel;
        }
        catch (DbUpdateException ex)
            when (ex.InnerException is PostgresException postgresEx
                && postgresEx.SqlState == "23505"
            )
        {
            throw new DuplicateCodeException();
        }
    }

    public async Task<ShortUrlModel?> GetByCodeAsync(string code)
    {
        return await _dbContext.ShortUrls.FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateVisitCountAsync(ShortUrlModel shortUrlModel)
    {
        shortUrlModel.VisitCount++;
        await SaveChangesAsync();
    }
}
