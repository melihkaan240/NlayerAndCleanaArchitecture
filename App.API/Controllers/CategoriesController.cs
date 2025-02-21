using App.Services;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using App.Services.Categories;
using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using App.Services.Products.UpdateStock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;

namespace App.API.Controllers
{

    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return CreateActionResult(await categoryService.GetAllListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            return CreateActionResult(await categoryService.GetByIdAsync(id));
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetCategoryWithProducts()
        {
            return CreateActionResult(await categoryService.GetCategoryWithProductAsync());
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetCategoryWithProducts(int id)
        {
            return CreateActionResult(await categoryService.GetCategoryWithProductAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            return CreateActionResult(await categoryService.CreateAsync(request));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryRequest request)
        {
            return CreateActionResult(await categoryService.UpdateAsync(id, request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return CreateActionResult(await categoryService.DeleteAsync(id));
        }
    }
}
