using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.BusinessLogic;
using BusinessLogic.Interfaces;


namespace BusinessLogic
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
