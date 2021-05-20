using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class CustomerModel
    {
        public int? cidx { get; set; }
        public int CustomerType { get; set; }
        public string CustomerCd { get; set; }
        [Required]
        public string CustName { get; set; }
        [Required]
        public string CustEngName { get; set; }
        [Required]
        public string CustAddress1 { get; set; }
        [Required]
        public string CustAddress2 { get; set; }
        [Required]
        public string CustCity { get; set; }
        [Required]
        public string CustState { get; set; }
        [Required]
        public string CustZip { get; set; }
        [Required]
        public string CustFullAddress { get; set; }
        [Required]
        public string OwnerName { get; set; }
        public string TaxId { get; set; }
        [Required]
        public string WPhone { get; set; }
        public string CPhone { get; set; }
        public string Fax { get; set; }
        public string SpecialClearanceNo { get; set; }

        public IList<CODE> customerTypeList { get; set; }
    }
}
