using TimeZone.BusinessLogic.Core;
using TimeZone.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone.Domain.Entities.Product;
using TimeZone.Domain;

namespace TimeZone.BusinessLogic
{
    class ProductBL : UserApi, IProduct
    {
        public ProductDetail GetProductDetail(int id)
        {
            return GetProductUser(id); 
        }
    }
}
