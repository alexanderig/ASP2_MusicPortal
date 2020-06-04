using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ASP2_MusicPortal.Models.Entities;

namespace ASP2_MusicPortal.Models.ViewModels
{
    public class ViewUser
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public bool isActive { get; set; }
        public bool isAdmin { get; set; }
        public string City { get; set; }
        
    }
}