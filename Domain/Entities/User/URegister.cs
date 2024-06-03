using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Enums;

namespace Time_Zone.Domain.Entities.User
{
    public class URegister
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDateTime { get; set; }
        public LevelAcces Role { get; set; }
    }
}
