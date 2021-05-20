using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class CodeModel
    {
        public int cdidx { get; set; }
        public int GROUPCD { get; set; }
        public string CD { get; set; }
        public string CDNAME { get; set; }
        public string REMARK { get; set; }
    }
}
