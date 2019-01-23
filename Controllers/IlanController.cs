using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmlakOtomasyonuProje.Controllers
{
    public class IlanController : BaseController
    {
        public ActionResult i()
        {

            if (IsLogon() == false) return RedirectToAction("index", "i");
            else if (((int)(CurentUser().MemberType)) < 4)
            {
                return RedirectToAction("index", "i");
            }
            var products = emlakDBEntities.Products.Where(x => x.Id != 0).ToList();
            return View(products.OrderByDescending(x => x.AddedDate).ToList());
        }
        
        public ActionResult Edit(int id = 0)
        {
            var product = emlakDBEntities.Products.FirstOrDefault(x => x.Id == id);
            var categories = emlakDBEntities.Category.Select(x => new SelectListItem()
            {
                Text = x.name,
                Value = x.id.ToString()
                
            }).ToList();
            ViewBag.Categories = categories;
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(DB.Products Ilan)
        {
            if (Ilan.ProductImageName1 == null)
                Ilan.ProductImageName1 = "";
         var productImagePath = string.Empty;
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file.ContentLength > 0)
                {
                    var folder = Server.MapPath("~/images/upload/Product");
                    var fileName = Guid.NewGuid() + ".jpg";
                    file.SaveAs(Path.Combine(folder, fileName));

                    var filePath = "images/upload/Product/" + fileName;
                    productImagePath = filePath;
                }
            }
            if (Ilan.Id > 0)
            {
                var dbIlan = emlakDBEntities.Products.FirstOrDefault(x => x.Id == Ilan.Id);
                dbIlan.Category_Id = (int)Ilan.CategoryID;
                dbIlan.CategoryID = Ilan.CategoryID;
                dbIlan.ModifiedDate = DateTime.Now;
                dbIlan.Description = Ilan.Description;
                
                dbIlan.Name = Ilan.Name;
                dbIlan.Price = Ilan.Price;
                dbIlan.oda = Ilan.oda;
                dbIlan.kat = Ilan.kat;
                dbIlan.metreKare = Ilan.metreKare;
                dbIlan.Address = Ilan.Address;
                dbIlan.state = Ilan.state;
                System.Diagnostics.Debug.WriteLine("a"+ Ilan.CategoryID+"a");
                System.Diagnostics.Debug.WriteLine("b" + Ilan.Category_Id + "b");
                if (string.IsNullOrEmpty(productImagePath) == false)
                {
                    dbIlan.ProductImageName1 = productImagePath;
                }
            }
            else
            {
                Ilan.Category_Id = (int)Ilan.CategoryID;
                Ilan.CategoryID = (int)Ilan.CategoryID;
                Ilan.AddedDate = DateTime.Now;
                Ilan.kId = base.CurrentUserId();
                
                emlakDBEntities.Entry(Ilan).State = System.Data.Entity.EntityState.Added;

            }

            emlakDBEntities.SaveChanges();

            return RedirectToAction("i");
        }
    }
}