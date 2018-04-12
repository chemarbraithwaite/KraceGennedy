using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KraceGennedy.Models
{
    public class Notify
    {
        public bool Sent { get; set; }
        public bool Today { get; set; }
        public bool Tomorrow { get; set; }
        public bool RainKngToday { get; set; }
        public bool RainKngTomorrow { get; set; }
        public bool RainMobdayToday { get; set; }
        public bool RainMobdayTomorrow { get; set; }

    }
}