using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebZadatak.Models;

namespace WebZadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementPController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //json objekat
        //"{"date":"7-3-2018"}"
        public IActionResult Get([FromBody] Newtonsoft.Json.Linq.JObject jdatum)
        {
            string date = jdatum.Value<string>("date");

            if (Convert.ToDateTime(date).CompareTo(DateTime.Now) > 0)
                return BadRequest("Prosledjeno je vreme vece od trenutnog!");

            ZadatakContext db = new ZadatakContext();

            List<DbelementP> ListdbelP = db.DbelementP
                .Where(el => el.DateAndTimeAdded > Convert.ToDateTime(date))
                .ToList<DbelementP>();

            var obj = JsonConvert.SerializeObject(ListdbelP);

            return this.Content(obj);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //"json lista i da se pazi na identifikacioni kod, mozda ima isti u bazu a primarni je kljuc"
        //"[{"RedniBroj":0,"IdentifikacioniKod":"2d9dfib0-cce9-4da5-af04-f58e4dfb9e5a","DateAndTimeAdded":"2019-08-04T21:03:18.903","P":2,"DbelementC":[{"Id":0,"Grupa":"t","Vrednost":6,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77","DBElementP":null},{"Id":0,"Grupa":"s","Vrednost":8,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77","DBElementP":null},{"Id":0,"Grupa":"n","Vrednost":6,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77"}]},
        //{"RedniBroj":1,"IdentifikacioniKod":"76affdcd-9d55-4j36-a082-9c48c5cd37dd","DateAndTimeAdded":"2019-08-04T21:03:19.383","P":2,"DbelementC":[{"Id":0,"Grupa":"t","Vrednost":6,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77","DBElementP":null},{"Id":0,"Grupa":"s","Vrednost":8,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77","DBElementP":null},{"Id":0,"Grupa":"n","Vrednost":6,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77"}]},
        //{"RedniBroj":2,"IdentifikacioniKod":"b0f66gda-1b44-4z51-838c-a3587d08f4ff","DateAndTimeAdded":"2019-08-04T21:03:19.387","P":2,"DbelementC":[{"Id":0,"Grupa":"t","Vrednost":6,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77","DBElementP":null},{"Id":0,"Grupa":"s","Vrednost":8,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77","DBElementP":null},{"Id":0,"Grupa":"n","Vrednost":6,"IdElementP":"d94076f1-c953-4899-9161-5a35cc505e77"}]}]"
        public IActionResult Post([FromBody] Newtonsoft.Json.Linq.JArray ListaPretraga)
        {
           
            List<DbelementP> list = 
                JsonConvert.DeserializeObject<List<DbelementP>>(ListaPretraga.ToString());

            ZadatakContext db = new ZadatakContext();

            foreach(DbelementP item in list)
            {
                db.DbelementP.Add(item);
            }
            db.SaveChanges();

            return this.Content(list[0].IdentifikacioniKod.ToString());
        }
    }
}