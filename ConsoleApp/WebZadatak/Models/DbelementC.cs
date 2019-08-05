using System;
using System.Collections.Generic;

namespace WebZadatak.Models
{
    public partial class DbelementC
    {
        public int Id { get; set; }
        public string Grupa { get; set; }
        public int? Vrednost { get; set; }
        public string IdElementP { get; set; }

        public DbelementP IdElementPNavigation { get; set; }
    }
}
