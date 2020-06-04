using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP2_MusicPortal.Models.Entities
{
    public class Genres
    {
        public Genres()
        {
            Phonotekas = new HashSet<Phonoteka>();
        }
        public int ID { get; set; }
        public string Genre { get; set; }
        public virtual ICollection<Phonoteka> Phonotekas { get; set; }
    }
}