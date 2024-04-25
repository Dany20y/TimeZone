using Domain.Entities.User;
using System.Web;
using Time_Zone.BusinessLogic;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User.Global;
using Time_Zone.Domain.Entities.User;
using BusinessLogic.Interfaces;
using BusinessLogic.Core;

namespace BusinessLogic
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

        public UserMinimal GetUserByCookie(string apiCookieValue)
        {
            return UserCookie(apiCookieValue);
        }

        /*        object ISession.ServiceToList()
                {
                    throw new System.NotImplementedException();
                }*/

    }
}
