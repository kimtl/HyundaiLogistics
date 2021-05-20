using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class RateModel
    {
        public int ridx { get; set; }
        public int cidx { get; set; }
        [Required]
        public DateTime applyDate { get; set; }
        [Required]
        public decimal baseRate { get; set; }
        public decimal extraRate { get; set; }
        public string weightType { get; set; }

        public IList<CODE> weightTypeList { get; set; }
     }
}
