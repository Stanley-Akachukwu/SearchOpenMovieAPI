using Microsoft.EntityFrameworkCore;
using SearchOpenMovies.Models.Entities;

namespace SearchOpenMovies.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
