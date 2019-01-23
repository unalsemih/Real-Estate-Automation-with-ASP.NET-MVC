using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmlakOtomasyonuProje.Models.Account
{
    public class RegisterModels
    {
        public RegisterModels()
        {
            Member = new DB.Members();
        }
        public DB.Members Member { get; set; }
        public string rePassword { get; set; }
    }
}