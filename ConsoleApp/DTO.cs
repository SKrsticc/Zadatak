using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak
{
    public class DTO
    {
        public void SaveElementP(ElementP elP, int p)
        {
            DBElementP dbelP = PretvoriObjekat(elP, p);

            ZadatakEntities db = new ZadatakEntities();
            db.DBElementPs.Add(dbelP);
            db.SaveChanges();
        }

        public DBElementP PretvoriObjekat(ElementP elP, int p)
        {
            DBElementP dbelP = new DBElementP();

            dbelP.RedniBroj = elP.RedniBroj;
            dbelP.IdentifikacioniKod = elP.IdentifikacioniKod.ToString();
            dbelP.DateAndTimeAdded = DateTime.Now;
            dbelP.p = p;
            for (int i = 0; i < elP.Elementi.Length; i++)
            {
                dbelP.DBElementCs.Add(PretvoriObjekat(elP.Elementi[i], elP.IdentifikacioniKod));
            }
            
            return dbelP;
        }

        public DBElementC PretvoriObjekat(ElementC elC, Guid IdKod)
        {
            DBElementC dbelC = new DBElementC();

            dbelC.Grupa = elC.Grupa.ToString();
            dbelC.Vrednost = elC.Vrednost;
            dbelC.IdElementP = IdKod.ToString();

            return dbelC;
        }

        public List<DBElementP> ReadResults(DateTime time)
        {
            if (time.CompareTo(DateTime.Now) > 0)
            {
                return null;
            }
            ZadatakEntities db = new ZadatakEntities();

            List<DBElementP> ListdbelP = db.DBElementPs
                .SqlQuery("select * from DBElementP where DateAndTimeAdded>@Time", new SqlParameter("@Time", time))
                .ToList<DBElementP>();

            return ListdbelP;
        }
    }
}
