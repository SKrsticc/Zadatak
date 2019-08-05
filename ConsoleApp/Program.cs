using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 0;
            int k = 0;
            Validator validator = new Validator();
            DTO dto = new DTO();

            Console.WriteLine("Izaberi ulaz: 1- tastatura, 2- konfiguracioni fajl");
            int ulaz = validator.ValidateUlaz();

            if (ulaz == 1) 
            {
                Console.WriteLine("Unesi n: ");
                n = validator.ValidatePozitivanBroj();

                Console.WriteLine("Unesi k: ");
                k = validator.ValidatePozitivanBroj();
            }
            else
            {
                using (StreamReader r = new StreamReader("../../ulaz.json"))
                {
                    var json = r.ReadToEnd();
                    dynamic obj = JsonConvert.DeserializeObject<dynamic>(json);

                    n = obj["n"];
                    k = obj["k"];

                    r.Close();
                }
            }

            ElementP[] el = new ElementP[n];

            for (int i = 0; i < n; i++)
            {
                el[i] = new ElementP(i, k);
                el[i].Stampaj();
            }

            Console.WriteLine("Izaberi: 1- pretraga po IdentifikacioniKod-u, 2- pretraga po p parametru");
            ulaz = validator.ValidateUlaz();

            if(ulaz == 1)
            {
                Console.WriteLine("\n\nUnesi IdentifikacioniKod za pretragu: ");
                Guid kod = Guid.Parse(Console.ReadLine());
                foreach(ElementP item in el)
                {
                    if (item.IdentifikacioniKod.CompareTo(kod) == 0)
                    {
                        item.Stampaj();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\n\nUnesi p za pretragu: ");
                int p = validator.ValidateBroj();

                Console.WriteLine("Izaberi izlaz unesi: json, baza");

                string output = validator.ValidateString();

                if(string.Compare(output,"json") == 0)
                {
                    using (StreamWriter w = new StreamWriter("../../izlaz.json"))
                    {
                        for (int i = 0; i < n; i++)
                        {
                            if (el[i].SumaVrednosti() > p)
                            {
                                var json = JsonConvert.SerializeObject(dto.PretvoriObjekat(el[i], p)
                                    , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                                w.WriteLine(json);
                            }
                        }
                        w.Close();
                    }
                }
                else if(string.Compare(output,"baza") == 0)
                {
                    ZadatakEntities db = new ZadatakEntities();
                    for (int i = 0; i < n; i++)
                    {
                        if (el[i].SumaVrednosti() > p)
                        {
                            dto.SaveElementP(el[i], p);
                        }
                    }
                }
            }
        }
    }
}
