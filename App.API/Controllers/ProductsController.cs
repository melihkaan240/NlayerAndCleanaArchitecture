using App.Services;
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

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
        {
            var serviceResult = await productService.GetPagedAllListAsync(pageNumber,pageSize);
            return CreateActionResult(serviceResult);
        }

        [HttpGet("{id:int}")]
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductRequest req) => CreateActionResult(await productService.UpdateAsync(id, req));

        [HttpPatch("stock")]
        public async Task<IActionResult> UpdateStock(UpdateProductStockRequest req)
        {
            return CreateActionResult(await productService.UpdateStockAsync(req));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceResult = await productService.DeleteAsync(id);

            return CreateActionResult(serviceResult);

        }
    }
}
