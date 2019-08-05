using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak
{
    public class ElementP
    {
        public int RedniBroj { get; set; }
        public Guid IdentifikacioniKod { get; set; }
        public ElementC[] Elementi { get; set; }
        
        public ElementP(int i, int k)
        {
            RedniBroj = i;
            IdentifikacioniKod = Guid.NewGuid();
            Elementi = new ElementC[k];
            for (int j = 0; j < k; j++)
                Elementi[j] = new ElementC();
        }

        public int SumaVrednosti()
        {
            int sum = 0;
            for (int i = 0; i < Elementi.Length; i++)
                sum += Elementi[i].Vrednost;

            return sum;
        }

        public void Stampaj()
        {
            Console.WriteLine("Redni broj: " + RedniBroj + ", IdentifikacioniKod: " + IdentifikacioniKod
                + " ,ukupna suma: " + SumaVrednosti() );
            Console.WriteLine("Elementi su: ");
            for (int j = 0; j < Elementi.Length; j++)
                Console.Write(Elementi[j].Grupa + " " + Elementi[j].Vrednost + "\n");
        }
    }
}
