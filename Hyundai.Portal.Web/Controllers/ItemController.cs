using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HyundaiPortal.Business.Model;
using HyundaiPortal.Business.Service;

namespace Hyundai.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        //
        // GET: /Item/
        public ItemService itemService;

        public ItemController()
        {
            itemService = new ItemService();
        }

        public ActionResult Check()
        {
            return View();
        }

        public ActionResult updateHBLConsole(string hblNo, int midx)
        {
            ResultModel result = new ResultModel();
            try
            {
                result = itemService.updateHBLConsole(hblNo, midx);
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
