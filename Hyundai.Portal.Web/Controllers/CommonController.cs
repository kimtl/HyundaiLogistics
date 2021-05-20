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
    public class CommonController : Controller
    {
        //
        // GET: /Customer/
        public CommonService commonService;

        public CommonController()
        {
            commonService = new CommonService();
        }

        public ActionResult getCodeList(ParameterModel param)
        {
            //Mapper.CreateMap<CODE, CodeModel>();
            var codeListDto = commonService.getCodeList(param).Where(c=>c.CD != null).ToList();
            var codeList = codeListDto.Select(c => Mapper.Map<CODE, CodeModel>(c));
            return Json(codeListDto, JsonRequestBehavior.AllowGet);
        }

    }
}
