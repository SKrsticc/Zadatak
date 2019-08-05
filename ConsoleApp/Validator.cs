using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak
{
    public class Validator
    {
        string line;
        public int ValidateUlaz()
        {
            int ulaz = 0;
            while (ulaz != 1 && ulaz != 2)
            {
                ulaz = ValidateBroj();
                if (ulaz != 1 && ulaz != 2)
                    Console.WriteLine("Lose unet broj, probaj ponovo...");
            }

            return ulaz;
        }

        public int ValidateBroj()
        {
            int ulaz;
            line = Console.ReadLine();
            while (!Int32.TryParse(line, out ulaz))
            {
                Console.WriteLine("Nije unet broj, probaj ponovo...");
                line = Console.ReadLine();
            }
            return ulaz;
        }

        public int ValidatePozitivanBroj()
        {
            int n = 0;
            while (n <= 0)
            {
                line = Console.ReadLine();
                while (!Int32.TryParse(line, out n))
                {
                    Console.WriteLine("Nije unet broj, probaj ponovo... ");
                    line = Console.ReadLine();
                }
                if (n <= 0)
                    Console.WriteLine("Nije unet pozitivan broj, probaj ponovo... ");
            }
            return n;
        }

        public string ValidateString()
        {
            while (string.Compare(line, "json") != 0 && string.Compare(line, "baza") != 0)
            {
                line = Console.ReadLine();
                if (string.Compare(line, "json") != 0 && string.Compare(line, "baza") != 0)
                    Console.WriteLine("Izaberi izlaz unesi: json, baza...");
            }
            return line;
        }
    }
}
