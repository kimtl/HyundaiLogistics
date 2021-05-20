using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class MBLModel
    {

        public MBLModel()
        {
            IssuingCarrierName = "HYUNDAI FREIGHTS INC";
            DptrCd = "JFK";
            DptrName = "NEW YORK, NY";
            DestCd = "ICN";
            DestName = "INCHEON, KOREA";
            AccountingInfo = "FREIGHT PREPAID";
            Currency = "USD";
            WTVALPPD = "PP";
            OTHERPPD = "PP";
            DeclaredValueForCarriage = "N.V.D";
            WTType = 5;
            NatureQuantityods = "\r\n" +
                                "CONSOLIDATION SHIPMENT\r\n" +
                                "AS PER ATTACHED\r\n" +
                                "CARGO MANIFEST";
            SignatureOfShipperOrAgent = "AS AGENT OF HYUNDAI FREIGHTS INC";
            SignatureOfIssuingCarrierOrAgent = "AS AGENT OF THE CARRIER ASIANA AIR";
        }
        public int midx
        {
            get;
            set;
        }

        [Required]
        public string mblNo
        {
            get;
            set;
        }

        public Nullable<int> status
        {
            get;
            set;
        }

        public Nullable<int> ShipperCd
        {
            get;
            set;
        }

        public string ShipperName
        {
            get;
            set;
        }

        public string ShipperAddress1
        {
            get;
            set;
        }

        public string ShipperAddress2
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

        public string ShipperZip
        {
            get;
            set;
        }
        [Required]
        public string ShipperFullAddress
        {
            get;
            set;
        }

        public Nullable<int> CneeCd
        {
            get;
            set;
        }

        public string CneeName
        {
            get;
            set;
        }

        public string CneePhone
        {
            get;
            set;
        }

        public string CneeAddress1
        {
            get;
            set;
        }

        public string CneeAddress2
        {
            get;
            set;
        }

        public string CneeCity
        {
            get;
            set;
        }

        public string CneeState
        {
            get;
            set;
        }

        public string CneeZip
        {
            get;
            set;
        }
        [Required]
        public string CneeFullAddress
        {
            get;
            set;
        }
        [Required]
        public string DptrCd
        {
            get;
            set;
        }
        [Required]
        public string DptrName
        {
            get;
            set;
        }
        [Required]
        public string DestCd
        {
            get;
            set;
        }
        [Required]
        public string DestName
        {
            get;
            set;
        }
        [Required]
        public string FltNo
        {
            get;
            set;
        }

        public string To1
        {
            get;
            set;
        }

        public string By1
        {
            get;
            set;
        }

        public string To2
        {
            get;
            set;
        }

        public string By2
        {
            get;
            set;
        }

        public Nullable<int> PKGS
        {
            get;
            set;
        }

        public Nullable<decimal> GrossWT
        {
            get;
            set;
        }

        public int WTType
        {
            get;
            set;
        }
        [Required]
        public string RateClass
        {
            get;
            set;
        }

        public Nullable<decimal> VWT
        {
            get;
            set;
        }

        public Nullable<decimal> CWT
        {
            get;
            set;
        }

        public Nullable<decimal> RateChange
        {
            get;
            set;
        }

        public Nullable<decimal> FltTotalAmt
        {
            get;
            set;
        }

        public string AccountingInfo
        {
            get;
            set;
        }

        public string HandlingInfo
        {
            get;
            set;
        }
        [Required]
        public string SignatureOfShipperOrAgent
        {
            get;
            set;
        }

        public string NatureQuantityods
        {
            get;
            set;
        }

        public string ByFirstCarrier
        {
            get;
            set;
        }

        public string IssuingCarrierName
        {
            get;
            set;
        }

        public string IATA
        {
            get;
            set;
        }

        public string AccountNo
        {
            get;
            set;
        }
        [Required]
        public Nullable<System.DateTime> FltDate
        {
            get;
            set;
        }
        [Required]
        public Nullable<System.DateTime> ArrivalDate
        {
            get;
            set;
        }

        public Nullable<int> NotNetiableCustCd
        {
            get;
            set;
        }

        public string Currency
        {
            get;
            set;
        }

        public string ChagsCode
        {
            get;
            set;
        }

        public string WTVALPPD
        {
            get;
            set;
        }

        public string WTVALCOL
        {
            get;
            set;
        }

        public string OTHERPPD
        {
            get;
            set;
        }

        public string OTHERCOL
        {
            get;
            set;
        }

        public string DeclaredValueForCarriage
        {
            get;
            set;
        }

        public string DeclaredValueForCustoms
        {
            get;
            set;
        }

        public Nullable<decimal> TotalAmt
        {
            get;
            set;
        }

        public string SignatureOfIssuingCarrierOrAgent
        {
            get;
            set;
        }
        [Required]
        public string NotNetiableText
        {
            get;
            set;
        }

        public IList<CODE> statusList { get; set; }
        public IList<CODE> weightTypeList { get; set; }
        public IList<CUSTOMER> shipperList { get; set; }
        public IList<CUSTOMER> consigneeList { get; set; }
        public IList<CUSTOMER> flightList { get; set; }
        public IList<OTHERCHARGE> otherChargeList { get; set; }
    }
}
