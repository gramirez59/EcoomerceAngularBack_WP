using E_CommerceCore.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_CommerceCore.Core.Managers.Products
{
    public interface IProductManager
    {
        Task<Product> CreateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<Product> GetProductAsync(int id);
        Task<List<Product>> GetProductsAsync();
        List<Product> GetProductsByCategory(int idCategory);
        List<Product> SearchProducts(string term);
        Task<Product> UpdateProductAsync(Product product);
    }
}