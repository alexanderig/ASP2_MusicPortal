using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP2_MusicPortal.Models.Entities
{
   
        public class Users
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public string Salt { get; set; }
            public bool isActive { get; set; }
            public bool isAdmin { get; set; }
            public virtual Cities Cities { get; set; }
        }
    
}