using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmlakOtomasyonuProje.DB;
using EmlakOtomasyonuProje.Models.Account;

namespace EmlakOtomasyonuProje.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Models.Account.RegisterModels user)
        {
            try
            {
                if (user.rePassword != user.Member.Password)
                {
                    throw new Exception("Şifreler aynı değildir");
                }
                if (emlakDBEntities.Members.Any(x => x.Email == user.Member.Email))
                {
                    throw new Exception("Zaten bu e-posta adresi kayıtlıdır.");
                }
                user.Member.MemberType = (int)DB.MemberTypes.Customer;
                user.Member.AddedDate = DateTime.Now;
                //System.Diagnostics.Debug.WriteLine("");
                //System.Diagnostics.Debug.WriteLine("");
                //System.Diagnostics.Debug.WriteLine("");

                //System.Diagnostics.Debug.WriteLine(""+ user.Member.Email);
                //System.Diagnostics.Debug.WriteLine("" + user.Member.Password);
                //System.Diagnostics.Debug.WriteLine("" + user.Member.AddedDate);
                //System.Diagnostics.Debug.WriteLine("" + user.Member.MemberType);
                //System.Diagnostics.Debug.WriteLine("");
                //System.Diagnostics.Debug.WriteLine("");
                //System.Diagnostics.Debug.WriteLine("");


                //member.MemberType = MemberTypes.Customer;
                emlakDBEntities.Members.Add(user.Member);
                emlakDBEntities.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.ReError = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Account.LoginModels model)
        {
            System.Diagnostics.Debug.WriteLine("login postt");
            try
            {
                var user = emlakDBEntities.Members.FirstOrDefault(x => x.Password == model.Member.Password && x.Email == model.Member.Email);
                if (user != null)
                {
                    Session["LogonUser"] = user;
                    System.Diagnostics.Debug.WriteLine("user.id = " + user.Id);
                    return RedirectToAction("index", "i");
                }
                else
                {
                    ViewBag.ReError = "Kullanici Bilgileriniz yanlış";
                    return View();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Catchhh");
                ViewBag.ReError = ex.Message;
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["LogonUser"] = null;
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Profil(int id = 0)
        {
            if (id == 0)
            {
                id = base.CurrentUserId();
                System.Diagnostics.Debug.WriteLine("iç : " + id);
            }
            var user = emlakDBEntities.Members.FirstOrDefault(x => x.Id == id);

            if (user == null) return RedirectToAction("index", "i");
            ProfilModels model = new ProfilModels()
            {
                Members = user
            };
            System.Diagnostics.Debug.WriteLine("Aa : " + user.Id);

            return View(model);
        }

        [HttpGet]
        public ActionResult ProfilEdit()
        {
            int id = base.CurrentUserId();
            var user = emlakDBEntities.Members.FirstOrDefault(x => x.Id == id);
            if (user == null) return RedirectToAction("index", "i");
            ProfilModels model = new ProfilModels()
            {
                Members = user
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult ProfilEdit(ProfilModels model)
        {

            try
            {
               int id = CurrentUserId();
                System.Diagnostics.Debug.WriteLine(" Kullanıcı İD :" + id);
                var updateMember = emlakDBEntities.Members.FirstOrDefault(x => x.Id == id);
                updateMember.ModifiedDate = DateTime.Now;
                updateMember.Bio = model.Members.Bio;
                updateMember.Name = model.Members.Name;
                updateMember.Surname = model.Members.Surname;

                if (string.IsNullOrEmpty(model.Members.Password) == false)
                {
                    updateMember.Password = model.Members.Password;
                }
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var folder = Server.MapPath("~/images/upload");
                        var fileName = Guid.NewGuid() + ".jpg";
                        file.SaveAs(Path.Combine(folder, fileName));

                        var filePath = "images/upload/" + fileName;
                        updateMember.ProfileImageName = filePath;
                    }
                }
                emlakDBEntities.SaveChanges();

                return RedirectToAction("Profil", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.MyError = ex.Message;
                int id = CurrentUserId();
                var viewModel = new Models.Account.ProfilModels()
                {
                    Members = emlakDBEntities.Members.FirstOrDefault(x => x.Id == id)
                };
                return View(viewModel);
            }
        }
    }









    /*
    [HttpPost]
        public ActionResult ProfilEdit (ProfilModels model)
        {
            try
            {
                int id = CurrentUserId();
                var updateMember = emlakDBEntities.Members.FirstOrDefault(x => x.Id == id);
                updateMember.ModifiedDate = DateTime.Now;
                updateMember.Bio = model.Members.Bio;
                updateMember.Name = model.Members.Name;
                updateMember.Surname = model.Members.Surname;

                if (string.IsNullOrEmpty(model.Members.Password) == false)
                {
                    updateMember.Password = model.Members.Password;
                }
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    var file = Request.Files[0];
                    if (file.ContentLength > 0)
                    {
                        var folder = Server.MapPath("~/images/upload");
                        var fileName = Guid.NewGuid() + ".jpg";
                        file.SaveAs(Path.Combine(folder, fileName));

                        var filePath = "images/upload/" + fileName;
                        updateMember.ProfileImageName = filePath;
                    }
                }
                emlakDBEntities.SaveChanges();

                return RedirectToAction("Profil", "Account");
            }
            catch (Exception ex)
            {
                ViewBag.MyError = ex.Message;
                int id = CurrentUserId();
                var viewModel = new Models.Account.ProfilModels()
                {
                    Members = emlakDBEntities.Members.FirstOrDefault(x => x.Id == id)
                };
                return View(viewModel);
            }
        }
        */
    }
