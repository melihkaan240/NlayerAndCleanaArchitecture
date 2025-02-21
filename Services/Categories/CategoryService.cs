using App.Repositories;
using App.Repositories.Categories;
using App.Repositories.Products;
using App.Services.Categories.Create;
using App.Services.Categories.Dto;
using App.Services.Categories.Update;
using App.Services.Products.Create;
using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
    {
        //crud operation
        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();

            var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);

            return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
        }

        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductAsync(int categoryId)
        {
            var category  = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if (category == null)
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("Kategori bulunamadı.",HttpStatusCode.NotFound);
            }
            var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
            return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductAsync()
        {
            var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();
            
            var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);
            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
        }
        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.Fail("kategori bulunamadı.", HttpStatusCode.NotFound);
            }

            var categoryAsDto = mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryAsDto);
        }

        public async Task<ServiceResult<int>> CreateAsync(CreateCategoryRequest request)
        {

            var anyCategories = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();

            if (anyCategories)
            {
                return ServiceResult<int>.Fail("Kategori ismi veritabanında bulunmaktadır", HttpStatusCode.BadRequest);
            }

            var newCategory = mapper.Map<Category>(request);
            await categoryRepository.AddAsync(newCategory);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.SuccessAsCreated(newCategory.Id, $"api/categories/{newCategory.Id}");
        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("Kategori bulunamadı", HttpStatusCode.NotFound);
            }

            var isCategoryExist = await categoryRepository.Where(x => x.Name == category.Name && x.Id != category.Id).AnyAsync();

            if (isCategoryExist)
            {
                return ServiceResult.Fail("Kategori ismi veritabanında bulunamadı", HttpStatusCode.BadRequest);
            }

            category = mapper.Map(request, category);
            categoryRepository.Update(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("kategori bulunamadı.", HttpStatusCode.NotFound);
            }
            categoryRepository.Delete(category);
            await unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
