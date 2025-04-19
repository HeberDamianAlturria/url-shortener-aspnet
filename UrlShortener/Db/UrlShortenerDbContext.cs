using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace UrlShortener.Db;

public class UrlShortenerDbContext : DbContext
{
    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options) { }

    public DbSet<ShortUrlModel> ShortUrls => Set<ShortUrlModel>();
}
