using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP2_MusicPortal.Models.Entities
{
    public class Phonoteka
    {
        public int ID { get; set; }
        public string TrackName { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Year { get; set; }
        public string TrackURL { get; set; }
        public int Bitrate { get; set; }
        public int Frequency { get; set; }
        public string Duration { get; set; }
        public string Mode { get; set; }
        public virtual Genres Genres { get; set; }
    }
}