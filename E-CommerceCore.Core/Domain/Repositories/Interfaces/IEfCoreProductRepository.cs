using System.Collections.Generic;
using E_CommerceCore.Core.Domain.Entities;

namespace E_CommerceCore.Core.Domain.Repositories.Interfaces
{
    public interface IEfCoreProductRepository : IRepository<Product>
    {
        List<Product> GetProductsByCategory(int idCategory);
        List<Product> SearchProducts(string term);
    }
}