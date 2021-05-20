using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class HBLModel
    {
        public int hidx
        {
            get;
            set;
        }

        public Nullable<int> midx
        {
            get;
            set;
        }

        public Nullable<int> Status
        {
            get;
            set;
        }

        [Required]
        public Nullable<int> cidx
        {
            get;
            set;
        }

        [Required]
        public string HblNo
        {
            get;
            set;
        }

        [Required]
        public Nullable<System.DateTime> OnBoardDate
        {
            get;
            set;
        }

        public string ShipperCd
        {
            get;
            set;
        }

        [Required]
        public string ShipperName
        {
            get;
            set;
        }
        [Required]
        public string ShipperPhone
        {
            get;
            set;
        }

        [Required]
        public string ShipperCity
        {
            get;
            set;
        }
        [Required]
        public string ShipperState
        {
            get;
            set;
        }
        [Required]
        public string ShipperZipCode
        {
            get;
            set;
        }
        [Required]
        public string ShipperAddress
        {
            get;
            set;
        }

        public string RefNo
        {
            get;
            set;
        }

        public string ConsigneeCD
        {
            get;
            set;
        }
        [Required]
        public string ConsigneeName
        {
            get;
            set;
        }
        [Required]
        public string ConsigneeEngName
        {
            get;
            set;
        }
        [Required]
        public string ConsigneePhone
        {
            get;
            set;
        }

        public string ConsigneeCellPhone
        {
            get;
            set;
        }
        [Required]
        public string ConsigneeZipCode
        {
            get;
            set;
        }
        [Required]
        public string ConsigneeZipAddress
        {
            get;
            set;
        }
        [Required]
        public string ConsigneeAddress
        {
            get;
            set;
        }

        public string EngZipaddress
        {
            get;
            set;
        }

        public string EngAddress
        {
            get;
            set;
        }

        public string juminNo
        {
            get;
            set;
        }

        public string Memo
        {
            get;
            set;
        }

        public Nullable<int> ConsigneeType
        {
            get;
            set;
        }

        public Nullable<short> Carton
        {
            get;
            set;
        }

        public Nullable<int> IsGeneralClearance
        {
            get;
            set;
        }
 
        public string ShipperEngName
        {
            get;
            set;
        }

        public Nullable<int> TransportType
        {
            get;
            set;
        }

        public Nullable<int> WeightType
        {
            get;
            set;
        }
        private Nullable<int> _weightType;

        public Nullable<decimal> Weight
        {
            get;
            set;
        }

        public string listClearanceCode
        {
            get;
            set;
        }

        public string SpecialClearanceNo
        {
            get;
            set;
        }

        public IList<PRODUCTITEM> itemList { get; set; }
        public IList<CODE> statusList { get; set; }
        public IList<CODE> clearanceList { get; set; }
        public IList<CODE> transportList { get; set; }
        public IList<CODE> weightTypeList { get; set; }
        public IList<CUSTOMER> customerList { get; set; }
        public IList<CODE> consigneeTypeList { get; set; }
    }
}
