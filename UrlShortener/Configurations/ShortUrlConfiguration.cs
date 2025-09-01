namespace UrlShortener.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Models;

public class ShortUrlConfiguration : IEntityTypeConfiguration<ShortUrlModel>
{
    public void Configure(EntityTypeBuilder<ShortUrlModel> builder)
    {
        builder.ToTable("short_urls");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(e => e.OriginalUrl).HasColumnName("original_url").IsRequired();

        builder.Property(e => e.ShortenedUrl).HasColumnName("shortened_url").IsRequired();

        builder.Property(e => e.Code).HasColumnName("code").IsRequired();

        builder.HasIndex(e => e.Code).IsUnique();

        builder.Property(e => e.VisitCount).HasColumnName("visit_count").IsRequired();

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
    }
}
