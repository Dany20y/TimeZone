using Time_Zone.BusinessLogic.Core;
using Time_Zone.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.Product;
using Time_Zone.Domain;

namespace Time_Zone.BusinessLogic
{
    class ProductBL : UserApi, IProduct
    {
        public ProductDetail GetProductDetail(int id)
        {
            return GetProductUser(id); 
        }
    }
}
