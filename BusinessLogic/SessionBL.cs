using TimeZone.BusinessLogic.Core;
using TimeZone.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone.Domain.Entities.User;
using TimeZone.Domain.Entities.Res;
using TimeZone.Domain.Entities.User.Global;

namespace TimeZone.BusinessLogic
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

    }
}
