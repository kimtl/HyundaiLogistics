using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class ParameterModel
    {
        public string searchKey { get; set; }
        public string searchText { get; set; }
        public int status { get; set; }
        public int cidx { get; set; }
        public int ridx { get; set; }
        public int hidx { get; set; }
        public int midx { get; set; }
        public int pidx { get; set; }
        public int InvoiceNo { get; set; }
        public int uidx { get; set; }
        public int idx { get; set; }
        public int GROUPCD { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

    }
}
