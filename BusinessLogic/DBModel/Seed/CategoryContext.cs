using Time_Zone.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Zone.BussinessLogic.DBModel
{
    class CategoryContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public CategoryContext() : base("name=Time_Zone")
        {
        }
    }
}
