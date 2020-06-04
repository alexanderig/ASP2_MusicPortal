using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP2_MusicPortal.Models.Entities
{
    public class Cities
    {
        public Cities()
        {
            Users = new HashSet<Users>();
        }
        public int ID { get; set; }
        public string CityName { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }

}