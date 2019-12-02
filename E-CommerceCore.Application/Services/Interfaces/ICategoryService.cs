using E_CommerceCore.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_CommerceCore.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<CategoryDto> CreateCategoryAsync(CategoryDto inputCategory);
        Task<List<CategoryDto>> GetAllAsync();
    }
}