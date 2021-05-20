using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HyundaiPortal.Business.Model;
using HyundaiPortal.Business.Service;
using log4net;
using AutoMapper;
using HyundaiPortal.Business;

namespace Hyundai.Controllers
{
    [Authorize]
    public class InvoiceController : BaseController
    {
        //
        // GET: /Customer/
        public InvoiceService invoiceService;
        public CommonService commonService;

        public InvoiceController()
        {
            invoiceService = new InvoiceService();
            commonService = new CommonService();
        }

        public ActionResult Generate()
        {
            return View();
        }

        public ActionResult getCustomerList(ParameterModel param)
        {
            var customerListDto = invoiceService.getCustomerList(param).OrderByDescending(c => c.FltDate).ToList();
            var customerListModel = customerListDto.Select(c=> new {
                                        midx = c.midx,
                                        customerName = c.CUSTOMER.CustName,
                                        amount = c.HBL.Sum(h=>h.Weight) * invoiceService.getRate(c.CneeCd??0,c.FltDate??DateTime.MinValue),
                                        status = c.CODE.CDNAME,
                                        onboardDate = c.FltDate
                                    });
            return Json(customerListModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Manage()
        {
            return View();
        }
    }
}
