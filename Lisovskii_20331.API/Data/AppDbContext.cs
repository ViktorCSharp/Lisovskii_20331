using Microsoft.EntityFrameworkCore;
using Lsiovskii_20331.Domain.Entities;

namespace Lisovskii_20331.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }


    }
}
