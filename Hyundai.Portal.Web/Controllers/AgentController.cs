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
    public class AgentController : BaseController
    {
        //
        // GET: /BL/
        public BLService blService;
        public CustomerService customerService;
        public CommonService commonService;

        public AgentController()
        {
            blService = new BLService();
            customerService = new CustomerService();
            commonService = new CommonService();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Hold()
        {
            return View();
        }

        public ActionResult Print()
        {
            return View();
        }

        public ActionResult ShipOut()
        {
            return View();
        }

        public ActionResult getHBLList(ParameterModel param)
        {
            param.cidx = (int)userInfo.cidx;
            var blListDto = blService.getHBLList(param).OrderByDescending(h => h.hidx);
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
