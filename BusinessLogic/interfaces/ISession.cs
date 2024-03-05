using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeZone.Domain.Entities.Res;
using TimeZone.Domain.Entities.User;
using TimeZone.Domain.Entities.User.Global;

namespace TimeZone.BusinessLogic.Interfaces
{
    public interface ISession
    {
        ActionStatus UserLogin(ULoginData data);
        LevelStatus CheckLevel(string key);
    }
}
