using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HyundaiPortal.Business;
using HyundaiPortal.Business.Model;
using HyundaiPortal.Business.Service;

namespace HyundaiPortal.Web.Controllers
{
    public class HBLController : ApiController
    {
        public CommonService commonService;
        public APIService apiService;

        public HBLController()
        {
            commonService = new CommonService();
            apiService = new APIService();
        }

        public ResultModel PostHBL(HBLAPIModel hbl)
        {
            ResultModel result = new ResultModel();
            //Confirm API Key
            var apiResult = apiService.checkAPIKey((int)hbl.cidx, hbl.apiKey);
            if (apiResult.ResultCode != 0)
            {
                result.ResultCode = -1;
                result.ResultMessage = "API Key Not Matched";
                return result;
            }

            //Process saving data
            result = apiService.setHBL(hbl);

            return result;
        }
    }
}
