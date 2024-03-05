using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone.Domain.Entities.Product;

namespace TimeZone.BusinessLogic.Interfaces
{
    public interface IProduct
    {
        ProductDetail GetProductDetail (int id);
    }
}
