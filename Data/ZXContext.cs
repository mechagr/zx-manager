using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZXManager.Models;

namespace ZXManager.Data
{
    public class ZXContext : IdentityDbContext
    {
        public ZXContext(DbContextOptions<ZXContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Adventure" },
                new Genre { Id = 2, Name = "Interactive Fiction" },
                new Genre { Id = 3, Name = "Action" }
            );

            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Tuckersoft" },
                new Publisher { Id = 2, Name = "Automata UK" },
                new Publisher { Id = 3, Name = "Level 9 Computing" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Title = "Bandersnatch", Year = 1984, Condition = "Good", Rating = 5, PublisherId = 1, GenreId = 1 },
                new Game { Id = 2, Title = "Deus Ex Machina", Year = 1984, Condition = "Excellent", Rating = 4, PublisherId = 2, GenreId = 3 },
                new Game { Id = 3, Title = "Lords of Time", Year = 1983, Condition = "Very Good", Rating = 4, PublisherId = 3, GenreId = 2 }
            );

            modelBuilder.Entity<Game>()
                .HasOne(v => v.Publisher)
                .WithMany(a => a.Games)
                .HasForeignKey(v => v.PublisherId);

            modelBuilder.Entity<Game>()
                .HasOne(v => v.Genre)
                .WithMany(c => c.Games)
                .HasForeignKey(v => v.GenreId);
        }
    }
}