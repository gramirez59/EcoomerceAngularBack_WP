using System.Collections.Generic;
using System.Threading.Tasks;
using E_CommerceCore.Core.Domain.Entities;

namespace E_CommerceCore.Core.Managers.Categories
{
    public interface ICategoryManager
    {
        Task<Category> CreateCategoryAsync(Category category);
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(int id);
    }
}