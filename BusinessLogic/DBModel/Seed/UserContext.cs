using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Entities.User;
using Time_Zone.Domain.User;


namespace Time_Zone.BusinessLogic.DBModel.Seed
{
    public class UserContext : DbContext
    {
        public UserContext() :
        base("name = Time_Zone")
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
