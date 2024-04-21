using Time_Zone.BusinessLogic.Core;
using Time_Zone.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User.Global;
using System.Web;
using Time_Zone.Domain.User;

namespace Time_Zone.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public ActionStatus UserLogin(ULoginData data)
        {
            return UserLogData(data);
        }
        public LevelStatus CheckLevel(string key)
        {
            return CheckLevelLogic(key);
        }

        public HttpCookie GenCookie(string loginCredential)
        {
            return Cookie(loginCredential);
        }

        public ActionStatus RegisterNewUserAction(URegisterData regData)
        {
            return RegisterUserAction(regData);
        }

    /*    object ISession.ServiceToList()
                {
                    throw new System.NotImplementedException();
                }
*/
    }
}
