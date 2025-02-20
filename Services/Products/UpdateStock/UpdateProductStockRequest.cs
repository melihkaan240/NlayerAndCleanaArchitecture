using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products.UpdateStock
{
    public record class UpdateProductStockRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
