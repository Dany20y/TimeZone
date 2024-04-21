using Time_Zone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Zone.Domain.Entities.User.Global
{
    public class LevelStatus
    {
        public LevelAcces Level { get; set;}
        public DateTime SessionTime { get; set;}
    }
}
