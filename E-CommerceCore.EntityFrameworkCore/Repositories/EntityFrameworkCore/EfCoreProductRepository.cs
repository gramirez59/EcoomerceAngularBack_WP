using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Domain.Repositories.EntityFrameworkCore;
using E_CommerceCore.Core.Domain.Repositories.Interfaces;
using E_CommerceCore.EntityFrameworkCore.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace E_CommerceCore.EntityFrameworkCore.Repositories.EntityFrameworkCore
{
    public class EfCoreProductRepository : EfCoreRepository<Product, ECommerceDbContext>, IEfCoreProductRepository
    {
        public EfCoreProductRepository(ECommerceDbContext context) : base(context)
        {

        }

        // We can add new methods specific to the Product repository here in the future
        public List<Product> GetProductsByCategory(int idCategory)
        {
            var query = this.DbSet.AsEnumerable();
            query = query.Where(p => p.IdCategory == idCategory);

            return query.ToList();
        }

        public List<Product> SearchProducts(string term)
        {
            var query = this.DbSet.AsEnumerable();
            if (!string.IsNullOrEmpty(term))
            {
                var cleanTerm = term.ToLowerInvariant().Trim();
                query = query.Where(p => p.Name.ToLowerInvariant().Contains(cleanTerm) || p.Description.ToLowerInvariant().Contains(cleanTerm));
            }

            return query.ToList();
        }
    }
}