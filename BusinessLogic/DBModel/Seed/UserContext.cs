using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.User;

namespace Time_Zone.BussinesLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() :
            base("name=Time_Zone") // connectionstring name define in your web.config
        {
        }

        public virtual DbSet<ULoginData> Users { get; set; }
    }
}
