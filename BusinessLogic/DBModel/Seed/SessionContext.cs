using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.User;

namespace Time_Zone.BussinessLogic.DBModel.Seed
{
    public class SessionContext: DbContext
    {
        public SessionContext() : base("name = Time_Zone")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }

    }
}
