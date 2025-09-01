namespace UrlShortener.Models;

public class ShortUrlModel
{
    public int Id { get; set; }

    public required string OriginalUrl { get; set; }

    public required string ShortenedUrl { get; set; }

    public required string Code { get; set; }

    public int VisitCount { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
