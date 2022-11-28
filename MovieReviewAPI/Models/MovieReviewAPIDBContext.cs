using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using MovieReviewAPI.Models;

namespace MovieReviewAPI.Models
{
    public class MovieReviewAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        
        public MovieReviewAPIDBContext(DbContextOptions<MovieReviewAPIDBContext> options, IConfiguration configuration) 
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserReviews>().HasAlternateKey(c => new { c.ShowId, c.UserId });
            modelBuilder.Entity<Users>().HasAlternateKey(c => new { c.Username });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("MovieReviewDatabase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public DbSet<Users> UserInfo { get; set; } = null!;
        public DbSet<TVShows> TVShows { get; set; } = null!;
        public DbSet<UserReviews> UserReviews { get; set; } = null!;
    }
}
