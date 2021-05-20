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
    public class CustomerController : BaseController
    {
        //
        // GET: /Customer/
        public CustomerService custService;
        public CommonService commonService;

        public CustomerController()
        {
            custService = new CustomerService();
            commonService = new CommonService();
        }

        public ActionResult CustomerList()
        {
            return View();
        }

        public ActionResult getCustomerList(ParameterModel param)
        {
            Mapper.CreateMap<CUSTOMER, CustomerModel>();
            var customerListDto = custService.getCustomerList(param).OrderByDescending(c=>c.cidx).ToList();
            var customerList = customerListDto.Select(c => Mapper.Map<CUSTOMER, CustomerModel>(c));
            return Json(customerList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerDetail(ParameterModel param)
        {
            var model = new CustomerModel();
            if (param.cidx != 0)
            {
                Mapper.CreateMap<CUSTOMER, CustomerModel>();
                var customerModel = custService.getCustomer(param);
                model = Mapper.Map<CUSTOMER, CustomerModel>(customerModel);
            }
            model.customerTypeList = commonService.getCodeList(new ParameterModel { GROUPCD = 1003 }).ToList();
            return View(model);
            
        }

        public ActionResult updateCustomer(CustomerModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<CustomerModel, CUSTOMER>();
                    CUSTOMER entity = Mapper.Map<CustomerModel, CUSTOMER>(model);
                    result = custService.updateCustomer(entity);
                }
                else
                {
                    result.ResultCode = -1;
                    result.ResultMessage = "Invalid Input";
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }

        public ActionResult getRateLIst(ParameterModel param)
        {
            Mapper.CreateMap<RATE, RateModel>();
            var rateListDto = custService.getRateList(param).ToList();
            var rateList = rateListDto.Select(c => Mapper.Map<RATE, RateModel>(c));
            return Json(rateList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerRateDetail(ParameterModel param)
        {
            var model = new RateModel();
            if (param.ridx != 0)
            {
                Mapper.CreateMap<RATE, RateModel>();
                var rateModel = custService.getRate(param);
                model = Mapper.Map<RATE, RateModel>(rateModel);
            }
            model.weightTypeList = commonService.getCodeList(new ParameterModel { GROUPCD = 1001 }).ToList();

            return View(model);
        }

        public ActionResult updateRate(RATE model)
        {
            ResultModel result = new ResultModel();
            try
            {
                model.RegId = userInfo.uidx;
                model.RegDate = DateTime.Now;
                result = custService.updateRate(model);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }

        public ActionResult getCustomerInfo(ParameterModel param)
        {
            var model = new CustomerModel();
            if (param.cidx != 0)
            {
                Mapper.CreateMap<CUSTOMER, CustomerModel>();
                var customerModel = custService.getCustomer(param);
                model = Mapper.Map<CUSTOMER, CustomerModel>(customerModel);
            }
            return Json(model, JsonRequestBehavior.AllowGet);

        }
    }
}
