using Time_Zone.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Enums;

namespace Domain.Entities.User
{
    public class UserMinimal
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public string LasIp { get; set; }
        public LevelAcces Level { get; set; }
    }
}