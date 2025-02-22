using App.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product,int>
    {
        //Task<Product> GetByIdAsync(int id);

        //public Task<List<Product>> GetByIdAsync(int id);
        public Task<List<Product>> GetTopPriceProductsAsync(int count);
    }
}
