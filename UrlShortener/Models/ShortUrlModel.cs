namespace UrlShortener.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Index(nameof(Code), IsUnique = true)]
public class ShortUrlModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string OriginalUrl { get; set; } = string.Empty;

    [Required]
    public string ShortenedUrl { get; set; } = string.Empty;

    [Required]
    public string Code { get; set; } = string.Empty;

    [Required]
    public int VisitCount { get; set; } = 0;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}