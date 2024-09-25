using Goke.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Goke.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<Card> Cards { get; set; } = default!;
        public DbSet<Person> People { get; set; }
    }
}
