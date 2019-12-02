using Microsoft.EntityFrameworkCore;
using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Domain.Entities.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace E_CommerceCore.EntityFrameworkCore.Configuration
{
    public class ECommerceDbContext : IdentityDbContext<User, Role, string>
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
           : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}