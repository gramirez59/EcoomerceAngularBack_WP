using System.Collections.Generic;
using System.Threading.Tasks;
using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Domain.Repositories.Interfaces;

namespace E_CommerceCore.Core.Managers.Categories
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IEfCoreCategoryRepository _categoryRepository;

        public CategoryManager(IEfCoreCategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await this._categoryRepository.AddAsync(category);

            return category;
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await this._categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await this._categoryRepository.GetAsync(id);
        }
    }
}