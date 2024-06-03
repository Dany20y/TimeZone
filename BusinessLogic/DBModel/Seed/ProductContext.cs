using Time_Zone.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Zone.BussinessLogic.DBModel
{
    class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductContext() : base("name=Time_Zone")
        {
        }
    }
}
