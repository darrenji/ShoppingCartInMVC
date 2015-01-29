using System.Collections.Generic;

namespace MvcApplication1.Models
{
    public class ProductsListVm
    {
        public ProductsListVm()
        {
            this.Products = new List<Product>();
        }
        public IEnumerable<Product> Products { get; set; }
    }
}