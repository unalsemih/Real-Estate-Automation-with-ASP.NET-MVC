using EmlakOtomasyonuProje.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakOtomasyonuProje.Models.i
{
    public class indexModel
    {
        public List<DB.Products> products { get; set; }
        public DB.Category Category { get; set; }
    }
}
//public List<DB.Products> products { get; set; }
