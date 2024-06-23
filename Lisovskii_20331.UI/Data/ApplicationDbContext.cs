using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lsiovskii_20331.Domain.Entities;

namespace Lisovskii_20331.UI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Lsiovskii_20331.Domain.Entities.Book> Book { get; set; } = default!;
    }
}
