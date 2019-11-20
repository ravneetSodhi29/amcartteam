using AmCart.IdentityServer.Model;
using Microsoft.EntityFrameworkCore;

namespace AmCart.IdentityServer.Contexts
{
    public class ISDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public ISDatabaseContext(DbContextOptions<ISDatabaseContext> options)
            : base(options)
        {

        }
    }
}
