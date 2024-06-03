using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Enums;

namespace Time_Zone.Domain.Entities.User
{
    public class ULoginResp
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public LevelAcces Level { get; set; }

    }
}

