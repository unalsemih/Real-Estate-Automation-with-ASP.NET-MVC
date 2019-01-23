using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakOtomasyonuProje.Models.i
{
    public class IlanlarimModel
    {
      public IlanlarimModel()
        {
            Ilanlar = new List<DB.Products>();
        }
        public List<DB.Products> Ilanlar { get; set; }
        public int ilanYayinDurum(int id)
        {
            if (Ilanlar[id].state == true)
                Ilanlar[id].state= false;
            else
                Ilanlar[id].state = true;
            try
            {
                EmlakOtomasyonuProje.DB.EmlakDBEntities1 emlakDB = new DB.EmlakDBEntities1();
                emlakDB.SaveChanges();
                return 1;
            }
            catch
            {
                return 0;
            }
        }
    }
}