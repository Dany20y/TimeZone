﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Zone.Domain.Enums;

namespace Domain.Entities.User
{
    public class ULoginData
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public LevelAcces level { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
