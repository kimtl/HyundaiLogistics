using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class UserModel
    {
        public int uidx { get; set; }
        [Required]
        public string userid { get; set; }
        [Required]
        public string password { get; set; }
        public int cidx { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int regId { get; set; }
        public int UserType { get; set; }
        public DateTime RegDate { get; set; }

        public IList<CODE> userTypeList { get; set; }
        public IList<CUSTOMER> customerList { get; set; }
    }
}
