using Time_Zone.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.BusinessLogic;

namespace Time_Zone.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
        public IProduct GetProductBL()
        {
            return new ProductBL();
        }
    }
}
