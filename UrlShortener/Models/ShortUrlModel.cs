namespace UrlShortener.Models;

public class ShortUrlModel
{
    public int Id { get; set; }
    public string OriginalUrl { get; set; } = string.Empty;
    public string ShortCode { get; set; } = string.Empty;
    public int VisitCount { get; set; } = 0;
}