using EmlakOtomasyonuProje.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmlakOtomasyonuProje.Models.i
{
    public class ilanEditModel
    {
        EmlakDBEntities1 emlakDB = new EmlakDBEntities1();
        public ilanEditModel()
        {
            Product = new DB.Products();
            kategoriler = new List<SelectListItem>();
            kategoriler = emlakDB.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }
        public DB.Products Product { get; set; }
        List<SelectListItem> kategoriler;
    }
}