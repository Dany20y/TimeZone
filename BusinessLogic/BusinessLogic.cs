using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.BusinessLogic;
using Time_Zone.BussinesLogic;
using Time_Zone.Domain.Entities.User;

namespace Time_Zone.BusinessLogic
{
    public class BusinessLogicService  
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
