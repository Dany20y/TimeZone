using BusinessLogic.Core;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
