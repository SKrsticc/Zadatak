using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak
{
    public class ElementC
    {
        public char Grupa { get; set; }
        public int Vrednost { get; set; }
        static Random _random = new Random();

        public ElementC()
        {
            Grupa = (char)('a' + _random.Next(0, 26));
            Vrednost = _random.Next(1, 9);
        }
    }
}
