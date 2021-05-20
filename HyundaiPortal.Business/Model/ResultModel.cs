using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;

namespace HyundaiPortal.Business.Model
{
    public class ResultModel
    {
        public ResultModel()
        {
            ResultCode = 0;
            ResultMessage = "";
        }

        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }
}
