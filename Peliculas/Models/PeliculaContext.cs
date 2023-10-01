using Microsoft.EntityFrameworkCore;

namespace Peliculas.Models
{
    public class PeliculaContext : DbContext
    {
        public PeliculaContext(DbContextOptions<PeliculaContext> options) : base(options)
        {
        }

        public DbSet<Pelicula> Peliculas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
