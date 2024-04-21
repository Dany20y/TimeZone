using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time_Zone.Models
{
    public class userLogin
    {
        public string Credential { get; set; }decimal 
        public string Password { get; set; }
        public DateTime LoginDateTime { get; set; }
        public string LoginIp { get; internal set; }
    }
}

