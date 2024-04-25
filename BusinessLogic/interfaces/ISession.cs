using Domain.Entities.User;
using System.Web;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User.Global;
using Time_Zone.Domain.Entities.User;

namespace BusinessLogic.Interfaces
{
    public interface ISession
    {
        ActionStatus UserLogin(ULoginData data);
        LevelStatus CheckLevel(string key);
        UserMinimal GetUserByCookie(string apiCookieValue);
        HttpCookie GenCookie(string loginCredential);
        ActionStatus RegisterNewUserAction(URegisterData regData);
    };
}
