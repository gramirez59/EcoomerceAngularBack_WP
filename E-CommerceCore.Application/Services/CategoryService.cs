using E_CommerceCore.Application.Dto;
using E_CommerceCore.Application.Services.Interfaces;
using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Managers.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceCore.Application.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryService : ControllerBase, ICategoryService
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryService(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        /// <summary>
        /// Creación de Categorias
        /// </summary>
        /// <returns>Categoria creada</returns>
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto inputCategory)
        {
            Category category = new Category() {
                Name = inputCategory.Name,
                Description = inputCategory.Description
            };

            category = await _categoryManager.CreateCategoryAsync(category);
            inputCategory.Id = category.Id;

            return inputCategory;
        }

        /// <summary>
        /// Consulta el listado de categorias
        /// </summary>
        /// <returns>Listado de categorias</returns>
        [Route("GetCategories")]
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            List<Category> categories = await _categoryManager.GetCategoriesAsync();

            var rProducts = categories.Select(p => new CategoryDto()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList();

            return rProducts;
        }
    }
}