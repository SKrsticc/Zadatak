using System;
using System.Collections.Generic;

namespace WebZadatak.Models
{
    public partial class DbelementP
    {
        public DbelementP()
        {
            DbelementC = new HashSet<DbelementC>();
        }

        public int RedniBroj { get; set; }
        public string IdentifikacioniKod { get; set; }
        public DateTime? DateAndTimeAdded { get; set; }
        public int P { get; set; }

        public ICollection<DbelementC> DbelementC { get; set; }
    }
}
