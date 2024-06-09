using System.Collections.Generic;
using Time_Zone.Domain.Entities.Product;

namespace Time_Zone.Models
{
    public class ProductsViewModel
    {
        public List<string> Categories { get; set; } // Nume de categorii
        public List<Product> Products { get; set; }
    }
}
