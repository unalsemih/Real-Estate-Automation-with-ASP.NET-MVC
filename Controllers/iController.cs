using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmlakOtomasyonuProje.DB;
using EmlakOtomasyonuProje.Models.i;

namespace EmlakOtomasyonuProje.Controllers
{
    public class iController : BaseController
    {
        EmlakDBEntities1 emlakDBEntities;
        public iController()
        {
            emlakDBEntities = new EmlakDBEntities1();
            //int i = Convert.ToInt32("adas");
        }
        [HttpGet]
        public ActionResult Index(int? id)
        {
            IQueryable<DB.Products> emlakList = emlakDBEntities.Products;
            DB.Category category = null;
            if (id>0)
            {
                if(id==1)//Konut Kısmı
                    emlakList = emlakList.Where(x => x.Category_Id == id || x.Category_Id == 5 || x.Category_Id == 6 || x.Category_Id == 7 || x.Category_Id == 8);
                else if (id == 2)//Bina Kısmı
                    emlakList = emlakList.Where(x => x.Category_Id == id || x.Category_Id == 9 || x.Category_Id == 10 );
                else if (id == 3)//İşyeri Kısmı
                    emlakList = emlakList.Where(x => x.Category_Id == id || x.Category_Id == 11 || x.Category_Id == 12 || x.Category_Id == 13 || x.Category_Id == 14 );
                else if (id == 4)//Arsa Kısmı
                    emlakList = emlakList.Where(x => x.Category_Id == id || x.Category_Id == 15 || x.Category_Id == 16);
                else if (id == 17)//Emlak Kısmı
                    emlakList = emlakList.Where(x => x.Category_Id == id || x.Category_Id == 5 || x.Category_Id == 6 || x.Category_Id == 7 || x.Category_Id == 8 || x.Category_Id == 9 || x.Category_Id == 10);

                else emlakList = emlakList.Where(x => x.Category_Id == id);
                category = emlakDBEntities.Category.FirstOrDefault(x => x.id == id);
            }
            var viewModel = new Models.i.indexModel()
            {
                products = emlakList.ToList(),
                Category = category

            };
            return View(viewModel);
        }
        [HttpGet]
        public ActionResult Product(int id)
        {
            var pro = emlakDBEntities.Products.FirstOrDefault(x=>x.Id==id);
            if (pro == null) return RedirectToAction("index", "i");

            ProductModels model = new ProductModels()
            {
                Product = pro,
             //   Comments = pro.Comments.ToList()
            };
            return View(model);
        }
        
        [HttpGet]
        public ActionResult Ilanlarim()
        {
            IlanlarimModel model = new IlanlarimModel();
            IQueryable<DB.Products> emlakList = emlakDBEntities.Products;
            int ida=2;
            if (CurrentUserId()!=0)
            model.Ilanlar= emlakList.Where(x => x.kId == ida).ToList();
            System.Diagnostics.Debug.WriteLine("model.ilanlar : " + model.Ilanlar.Count);
            return View(model);

        }
        [HttpGet]
        public ActionResult onState(int id)
        {
            
            var products  = emlakDBEntities.Products.FirstOrDefault(x => x.Id == id);
            if (products != null)
                products.state = true;
            else
                System.Diagnostics.Debug.WriteLine("bulunamadi2");
            emlakDBEntities.SaveChanges();
            System.Diagnostics.Debug.WriteLine("onState");
            return RedirectToAction("Ilanlarim", "i");
        }
        [HttpGet]
        public ActionResult offState(int id)
        {

            var products = emlakDBEntities.Products.FirstOrDefault(x => x.Id == id);
            if(products!=null)
            products.state = false;
            else
                System.Diagnostics.Debug.WriteLine("bulunamadi1");
            emlakDBEntities.SaveChanges();
            System.Diagnostics.Debug.WriteLine("offState");
            return RedirectToAction("Ilanlarim", "i");
        }
        [HttpGet]
        public ActionResult removeIlan(int id)
        {

            var products = emlakDBEntities.Products.FirstOrDefault(x => x.Id == id);
            if (products != null)
                emlakDBEntities.Products.Remove(products);
            else
                System.Diagnostics.Debug.WriteLine("bulunamadi1");
            emlakDBEntities.SaveChanges();
            System.Diagnostics.Debug.WriteLine("offState");
            return RedirectToAction("Ilanlarim", "i");
        }
    }
}