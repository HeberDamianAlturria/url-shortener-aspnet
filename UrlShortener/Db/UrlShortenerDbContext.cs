using Microsoft.EntityFrameworkCore;
using UrlShortener.Configurations;
using UrlShortener.Models;

namespace UrlShortener.Db;

public class UrlShortenerDbContext : DbContext
{
    public DbSet<ShortUrlModel> ShortUrls => Set<ShortUrlModel>();

    public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ShortUrlConfiguration());
    }
}
