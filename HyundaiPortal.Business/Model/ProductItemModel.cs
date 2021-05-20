using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class ProductItemModel
    {
        public int pidx
        {
            get;
            set;
        }

        public int hidx
        {
            get;
            set;
        }

        [Required]
        public string itemName
        {
            get;
            set;
        }

        [Required]
        public decimal itemAmt
        {
            get;
            set;
        }

        [Required]
        public int itemQty
        {
            get;
            set;
        }

        public decimal itemTotalAmt
        {
            get;
            set;
        }

        public string itemBrand
        {
            get;
            set;
        }

        public string url
        {
            get;
            set;
        }
    }
}
