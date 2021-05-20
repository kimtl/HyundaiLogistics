using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HyundaiPortal.Business.Service;
using HyundaiPortal.Business.Model;
using AutoMapper;
using HyundaiPortal.Business;
using System.IO;
using HyundaiPortal.Business.Utility;

namespace Hyundai.Controllers
{
    [Authorize]
    public class BLController : BaseController
    {
        //
        // GET: /BL/
        public BLService blService;
        public CustomerService customerService;
        public CommonService commonService;

        public BLController()
        {
            blService = new BLService();
            customerService = new CustomerService();
            commonService = new CommonService();
        }

        public ActionResult Master()
        {
            return View();
        }

        public ActionResult getMBLList(ParameterModel param)
        {
            var blListDto = blService.getMBLList(param).OrderByDescending(h => h.midx); ;
            return Json(blListDto.Select(u => new { u.midx, u.mblNo, ShipperName = u.CUSTOMER.CustName, CneeName = u.CUSTOMER1.CustName, u.DestName, u.TotalAmt, u.CreateDate }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult MBLDetail(ParameterModel param)
        {
            var model = new MBLModel();
            if (param.midx != 0)
            {
                Mapper.CreateMap<MBL, MBLModel>();
                var mblDto = blService.getMBL(param);
                model = Mapper.Map<MBL, MBLModel>(mblDto);
                model.otherChargeList = mblDto.OTHERCHARGE.ToList();
            }
            else
            {
                model.otherChargeList = new List<OTHERCHARGE>();
                model.otherChargeList.Add(new OTHERCHARGE() { chargeCd = "FSC"});
                model.otherChargeList.Add(new OTHERCHARGE() { chargeCd = "SCR"});
            }
            var customerList = customerService.getCustomerList(new ParameterModel()).ToList();
            if (customerList != null)
            {
                model.shipperList = customerList.Where(c => c.CustomerType == 13).ToList();
                model.consigneeList = customerList.Where(c => c.CustomerType == 14).ToList();
                model.flightList = customerList.Where(c => c.CustomerType == 15).ToList();
            }
            model.statusList = commonService.getCodeList(new ParameterModel { GROUPCD = 1007 }).ToList();
            model.weightTypeList = commonService.getCodeList(new ParameterModel { GROUPCD = 1001 }).ToList();
            
            if (model.otherChargeList.Count() < 5)
            {
                int addCnt = 5 - model.otherChargeList.Count();
                for (int i = 0; i < addCnt; i++)
                {
                    model.otherChargeList.Add(new OTHERCHARGE());
                }
            }
            return View(model);
        }

        public ActionResult getMblStatus(ParameterModel param)
        {
            var model = new MBLModel();
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (param.midx != 0)
            {
                var mblDto = blService.getMBL(param);
                result.Data = new { status = mblDto.CODE.CDNAME, statusCode = mblDto.CODE.cdidx };
            }

            return result;
        }

        public ActionResult updateMBL(MBLModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                Mapper.CreateMap<MBLModel, MBL>();
                MBL entity = Mapper.Map<MBLModel, MBL>(model);
                entity.CreateId = userInfo.uidx;
                result = blService.updateMBL(entity);
                blService.updateMBLOtherCharge(Convert.ToInt32(result.ResultMessage), model.otherChargeList);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            Response.Redirect("/BL/Master");

            return View();
        }

        public ActionResult deleteMBL(int midx)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = blService.deleteMBL(midx);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }

        public ActionResult MBLPDF(ParameterModel param)
        {
            var model = new MBL();
            IList<OTHERCHARGE> list = new List<OTHERCHARGE>();
            if (param.midx != 0)
            {
                model = blService.getMBL(param);
                list = model.OTHERCHARGE.ToList();
                GeneratePDF PDF = new GeneratePDF(model, list);
                PDF.sourcePdfPath = Server.MapPath(ConfigurationManager.AppSettings["SOURCE_PDF_PATH"]);
                PDF.savePdfPath   = Server.MapPath(ConfigurationManager.AppSettings["SAVE_PDF_PATH"]);
                PDF.GenerateMBLPDF();

                JsonResult result = new JsonResult();
                JsonObject jsonObject = new JsonObject();
                jsonObject.SmallUrl = ConfigurationManager.AppSettings["SAVE_PDF_PATH"] + model.mblNo.Trim() + ".pdf";
                jsonObject.isDesktop = true;
                result.Data = jsonObject;
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                return result;

            }
            return View();
        }

        public ActionResult House()
        {
            ViewBag.statusList = commonService.getCodeList(new ParameterModel { GROUPCD = 1005 }).ToList();
            return View();
        }

        public ActionResult getHBLList(ParameterModel param)
        {
            if (Request.Cookies["userType"].Value == "28")
            {
                param.cidx = (int)userInfo.cidx;
            }
            var blListDto = blService.getHBLList(param).OrderByDescending(h=>h.hidx);
            return Json(blListDto.Select(u => new { u.hidx, u.MBL.mblNo, u.midx, u.HblNo, Status = u.CODE.CDNAME, u.OnBoardDate, u.ShipperName, u.ConsigneeName, u.CreateDate,u.Weight, WeightType=u.CODE3.CDNAME }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult HBLDetail(ParameterModel param)
        {
            var model = new HBLModel();
            if (param.hidx != 0)
            {
                Mapper.CreateMap<HBL, HBLModel>();
                var mblDto = blService.getHBL(param);
                model = Mapper.Map<HBL, HBLModel>(mblDto);
                model.itemList = mblDto.PRODUCTITEM.ToList();
            }
            else
            {
                model.itemList = new List<PRODUCTITEM>();
            }
            
            model.customerList = customerService.getCustomerList(new ParameterModel()).ToList();
            model.statusList = commonService.getCodeList(new ParameterModel { GROUPCD = 1005 }).ToList();
            model.clearanceList = commonService.getCodeList(new ParameterModel { GROUPCD = 1000 }).ToList();
            model.transportList = commonService.getCodeList(new ParameterModel { GROUPCD = 1002 }).ToList();
            model.weightTypeList = commonService.getCodeList(new ParameterModel { GROUPCD = 1001 }).ToList();
            model.consigneeTypeList = commonService.getCodeList(new ParameterModel { GROUPCD = 1004 }).ToList();
            return View(model);
        }

        public ActionResult updateHBL(HBLModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                Mapper.CreateMap<HBLModel, HBL>();
                HBL entity = Mapper.Map<HBLModel, HBL>(model);
                entity.CreateId = userInfo.uidx;
                result = blService.updateHBL(entity);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            Response.Redirect("/BL/House");

            return View();
        }

        public ActionResult updateHBLConsole(int hidx, int midx)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = blService.updateHBLConsole(hidx, midx);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }


        public ActionResult updateHBLConsoleList(int midx, int[] hidx)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = blService.updateHBLConsoleList(midx, hidx);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }

        public ActionResult HouseRegister()
        {
            return View();
        }

        public ActionResult uploadHouseBL(HttpPostedFileBase file)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                    result = blService.uploadHouseBL(userInfo, path);
                }
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
            }
            Response.Redirect(string.Format("/BL/HouseRegister?ResultCode={0}&ResultMessage={1}", result.ResultCode, result.ResultMessage));
            
            return Json(result);
        }

        public ActionResult HouseItemDetail(ParameterModel param)
        {
            var model = new ProductItemModel();
            if (param.pidx != 0)
            {
                Mapper.CreateMap<PRODUCTITEM, ProductItemModel>();
                var itemDto = blService.getProductItem(param);
                model = Mapper.Map<PRODUCTITEM, ProductItemModel>(itemDto);
            }
            else
            {
                model.hidx = param.hidx;
            }
            
            return View(model);
        }

        public ActionResult updateHouseItem(ProductItemModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                Mapper.CreateMap<ProductItemModel, PRODUCTITEM>();
                PRODUCTITEM entity = Mapper.Map<ProductItemModel, PRODUCTITEM>(model);
                result = blService.updateHouseItem(entity);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }

        public ActionResult deleteHouseItem(ParameterModel param)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = blService.deleteHouseItem(param);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }

        public ActionResult deleteHBL(int[] hidx)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = blService.deleteHBL(hidx);
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }

            return Json(result);
        }
    }
}
