using E_CommerceCore.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_CommerceCore.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateProductAsync(ProductDto inputProduct);
        Task<List<ProductDto>> GetAllAsync();
        List<ProductDto> GetProductsByCategory(int idCategory);
        List<ProductDto> SearchProducts(string term);
    }
}