using App.Services;
using App.Services.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;

namespace App.API.Controllers
{

    public class ProductsController : CustomBaseController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceResult = await productService.GetAllListAsync();
            return CreateActionResult(serviceResult);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceResult = await productService.GetByIdAsync(id);

            return CreateActionResult(serviceResult);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest req)
        {
            var serviceResult = await productService.CreateAsync(req);

            return CreateActionResult(serviceResult);

        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, UpdateProductRequest req)
        {
            var serviceResult = await productService.UpdateAsync(id, req);

            return CreateActionResult(serviceResult);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await productService.DeleteAsync(id);

            return CreateActionResult(serviceResult);

        }
    }
}
