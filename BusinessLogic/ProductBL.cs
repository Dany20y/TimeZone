using BusinessLogic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeZone.BusinessLogic
{
    class ProductBL : UserApi, IProduct
    {
        public ProductDetail GetDetailProduct(int id)
        {
            return GetProdUser(id);
        }
    }
}
