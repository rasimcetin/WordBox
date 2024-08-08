using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WordBox.Api;

public class WordBoxDbContext : DbContext
{
    public WordBoxDbContext(DbContextOptions<WordBoxDbContext> options) : base(options)
    {
    }

    public DbSet<Word> Words { get; set; }

    public DbSet<WordMeaning> WordMeanings { get; set; }

    public DbSet<Language> Languages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>()
            .HasMany<Word>()
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Word>()
            .HasMany(w => w.Meanings)
            .WithOne(wm => wm.Word)
            .HasForeignKey(wm => wm.WordId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
