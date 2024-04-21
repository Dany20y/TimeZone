using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Enums;
using TimeZone.Domain.Entities.Res;
using TimeZone.Domain.Entities.User;
using TimeZone.Domain.Entities.User.Global;
using BusinessLogic.Core;
using System.Web;

namespace TimeZone.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ActionStatus UserLogin(ULoginData data);
        LevelStatus CheckLevel(string key);
        HttpCookie GenCookie(string loginCredential);
        ActionStatus RegisterNewUserAction(URegisterData regData);
    }
}
