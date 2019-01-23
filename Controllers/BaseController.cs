using EmlakOtomasyonuProje.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmlakOtomasyonuProje.Controllers
{
    public class BaseController : Controller
    {
        protected EmlakDBEntities1 emlakDBEntities;
        // GET: Base
        public BaseController()
        {
            emlakDBEntities = new EmlakDBEntities1();
        
        }
        protected DB.Members CurentUser()
        {
            if (Session["LogonUser"] == null) return null;
            return (DB.Members)Session["LogonUser"];
        }
        protected int CurrentUserId()
        {
            if (Session["LogonUser"] == null) return 0;
            return ((DB.Members)Session["LogonUser"]).Id;
        }
        protected bool IsLogon()
        {
            if (Session["LogonUser"] == null)
                return false;
            else
                return true;
        }
    }
}