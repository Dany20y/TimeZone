using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.Product;

namespace Time_Zone.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ProductDetail GetProductDetail (int id);
    }
}
