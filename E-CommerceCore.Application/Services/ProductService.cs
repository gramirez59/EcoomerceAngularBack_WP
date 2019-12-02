using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_CommerceCore.Application.Dto;
using E_CommerceCore.Application.Services.Interfaces;
using E_CommerceCore.Core.Domain.Entities;
using E_CommerceCore.Core.Managers.Categories;
using E_CommerceCore.Core.Managers.Products;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceCore.Application.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductService : ControllerBase, IProductService
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IProductManager _productManager;

        public ProductService(ICategoryManager categoryManager, IProductManager productManager)
        {
            _categoryManager = categoryManager;
            _productManager = productManager;
        }

        /// <summary>
        /// Creación de Categorias
        /// </summary>
        /// <returns>Categoria creada</returns>
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ProductDto> CreateProductAsync(ProductDto inputProduct)
        {
            Category category = await _categoryManager.GetCategoryAsync(inputProduct.IdCategory);

            if (category == null)
            {
                throw new Exception("The category does not exist.");
            }

            Product product = new Product()
            {

                IdCategory = inputProduct.IdCategory,
                Name = inputProduct.Name,
                Description = inputProduct.Description,
                Price = inputProduct.Price,
                Stock = inputProduct.Stock
            };

            product = await _productManager.CreateProductAsync(product);
            inputProduct.Id = product.Id;

            return inputProduct;
        }

        /// <summary>
        /// Consulta el listado de productos
        /// </summary>
        /// <returns>Listado de productos</returns>
        [Route("GetProducts")]
        public async Task<List<ProductDto>> GetAllAsync()
        {
            List<Product> products = await _productManager.GetProductsAsync();

            var rProducts = products.Select(p => new ProductDto() {
                Id = p.Id,
                IdCategory = p.IdCategory,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();

            return rProducts;
        }

        /// <summary>
        /// Consulta el listado de productos por Categoria
        /// </summary>
        /// <param name="IdCategory">Identificador de la Categoria a buscar</param>
        /// <returns>Listado de productos</returns>
        [HttpPost]
        [Route("GetProductsByCategory")]
        public List<ProductDto> GetProductsByCategory(int idCategory)
        {
            List<Product> products = _productManager.GetProductsByCategory(idCategory);

            var rProducts = products.Select(p => new ProductDto()
            {
                Id = p.Id,
                IdCategory = p.IdCategory,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();

            return rProducts;
        }

        /// <summary>
        /// Consulta el listado de productos filtrados por Nombre o Descripción
        /// </summary>
        /// <param name="term">Termino a buscar</param>
        /// <returns>Listado de productos</returns>
        [HttpPost]
        [Route("SearchProducts")]
        public List<ProductDto> SearchProducts(string term)
        {
            List<Product> products = _productManager.SearchProducts(term);

            var rProducts = products.Select(p => new ProductDto()
            {
                Id = p.Id,
                IdCategory = p.IdCategory,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            }).ToList();

            return rProducts;
        }
    }
}