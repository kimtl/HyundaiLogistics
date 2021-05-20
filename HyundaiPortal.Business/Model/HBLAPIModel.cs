using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Runtime.Serialization;

namespace HyundaiPortal.Business.Model
{
    public class HBLAPIModel
    {
        public string apiKey
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

        public Nullable<int> cidx
        {
            get;
            set;
        }

        public string HblNo
        {
            get;
            set;
        }

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

        public string ShipperName
        {
            get;
            set;
        }

        public string ShipperPhone
        {
            get;
            set;
        }

        public string ShipperCity
        {
            get;
            set;
        }

        public string ShipperState
        {
            get;
            set;
        }
        
        public string ShipperZipCode
        {
            get;
            set;
        }
        
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
        
        public string ConsigneeName
        {
            get;
            set;
        }
        
        public string ConsigneeEngName
        {
            get;
            set;
        }
        
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
        
        public string ConsigneeZipCode
        {
            get;
            set;
        }
        
        public string ConsigneeZipAddress
        {
            get;
            set;
        }
        
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
    }
}
