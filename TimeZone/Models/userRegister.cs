﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimeZone.Domain.Enums;

namespace TimeZone.Models
{
    public class userRegister
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
        public LevelAcces level { get; set; }
    }
}