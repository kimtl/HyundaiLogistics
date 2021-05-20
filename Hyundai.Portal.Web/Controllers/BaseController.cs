using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HyundaiPortal.Business.Service;
using HyundaiPortal.Business;
using HyundaiPortal.Business.Model;
using System.Web.UI;
using System.Web.Security;

namespace Hyundai.Controllers
{
    public class BaseController : Controller
    {
        public AccountService accService;
        public USER userInfo {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return accService.getUserInfo(User.Identity.Name);
                }
                else
                {
                    return null;
                }
            }
        }

        public BaseController()
        {
            accService = new AccountService();
        }
    }
}
