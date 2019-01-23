using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakOtomasyonuProje.Models.i
{
    public class ProductModels
    {
        public ProductModels()
        {
            Product = new DB.Products();
  
        }
        public DB.Products Product { get; set; }
      
    }
}