using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Enums;
using Time_Zone.Domain.Entities.Res;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.Entities.User.Global;
using BusinessLogic.Core;
using System.Web;
using Time_Zone.Domain.User;

namespace Time_Zone.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ActionStatus UserLogin(ULoginData data);
        LevelStatus CheckLevel(string key);
       /* HttpCookie GenCookie(string loginCredential);*/
        ActionStatus RegisterNewUserAction(URegisterData regData);
    }
}
