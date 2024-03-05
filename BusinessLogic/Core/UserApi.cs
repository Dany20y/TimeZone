using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone.Domain.Entities.User;
using TimeZone.Domain.Entities.Res;
using TimeZone.Domain.Entities.User.Global;
using TimeZone.Domain.Entities.Product;
using System.Net.Http.Headers;



namespace TimeZone.BusinessLogic.Core
{
    public class UserApi
    {
        internal ActionStatus UserLogData(ULoginData data)
        {
            return new ActionStatus();
        }
        
        internal LevelStatus CheckLevelLogic(string keySession)
        {
            return new LevelStatus();
        }

        internal ProductDetail GetProductUser(int id) 
        {
            return new ProductDetail();
        }
    }
}
