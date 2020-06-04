using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ASP2_MusicPortal.Models.Entities;

namespace ASP2_MusicPortal.Models.ViewModels
{
    public class ViewRegister
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        [Required]
        public int CityID { get; set; } 
        public virtual Cities City { get; set; }

        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name ="Password check")]
        public string PasswordDouble { get; set; }


    }
}