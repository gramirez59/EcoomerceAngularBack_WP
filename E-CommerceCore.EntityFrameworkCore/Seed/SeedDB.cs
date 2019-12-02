using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using E_CommerceCore.Core.Domain.Entities.Security;
using E_CommerceCore.EntityFrameworkCore.Configuration;
using E_CommerceCore.Core.Domain.Entities;

namespace E_CommerceCore.EntityFrameworkCore.Seed
{
    public class SeedDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ECommerceDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            context.Database.EnsureCreated();
            if (!context.Users.Any())
            {
                User user = new User()
                {
                    Email = "admin@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "Secreto01*").Result;

                //Category category = new Category()
                //{
                //    Name = "Categoria 1",
                //    Description = "Categoria 1";
                //};
                
                if (result.Succeeded)
                {
                    //userManager.AddToRoleAsync(user, "Admin").Wait();
                }
                context.SaveChangesAsync();
            }
        }
    }
}