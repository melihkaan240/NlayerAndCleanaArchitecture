    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace App.Repositories.Products
    {
        public class ProductRepository(AppDBContext context) : GenericRepository<Product>(context), IProductRepository
        {
        // task from result dikkat et 
        //    public Task<Product> GetByIdAsync(int id)
        //    {
        //     return Task.FromResult(Context.Products.FirstOrDefault(x => x.Id == id));
        //}

            public Task<List<Product>> GetTopPriceProductsAsync(int count)
            {
                return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
            }

       
    }
    }
