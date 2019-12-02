using System.Collections.Generic;
using System.Threading.Tasks;
using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Domain.Repositories.Interfaces;

namespace E_CommerceCore.Core.Managers.Products
{
    public class ProductManager : IProductManager
    {
        private readonly IEfCoreProductRepository _productRepository;

        public ProductManager(IEfCoreProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await this._productRepository.AddAsync(product);

            return product;
        }

        public async Task DeleteProductAsync(Product product)
        {
            await this._productRepository.DeleteAsync(product.Id);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await this._productRepository.GetAsync(id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await this._productRepository.GetAllAsync();
        }

        public List<Product> GetProductsByCategory(int idCategory)
        {
            return this._productRepository.GetProductsByCategory(idCategory);
        }

        public List<Product> SearchProducts(string term)
        {
            return this._productRepository.SearchProducts(term);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await this._productRepository.UpdateAsync(product);
        }
    }
}