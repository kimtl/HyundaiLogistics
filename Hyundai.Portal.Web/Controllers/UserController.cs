using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HyundaiPortal.Business.Model;
using HyundaiPortal.Business;
using AutoMapper;
using HyundaiPortal.Business.Service;

namespace Hyundai.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        //
        // GET: /User/
        public UserService userService;
        public CommonService commonService;
        public CustomerService customerService;

        public UserController()
        {
            userService = new UserService();
            commonService = new CommonService();
            customerService = new CustomerService();
        }

        public ActionResult UserList()
        {
            return View();
        }

        public ActionResult getUserList(ParameterModel param)
        {
            var userListDto = userService.getUserList(param).OrderByDescending(u=>u.uidx);
            return Json(userListDto.Select(u => new { uidx = u.uidx, userid = u.userid, UserName = u.UserName, CustName = u.CUSTOMER.CustName, UserType = u.CODE.CDNAME, UserEmail = u.UserEmail, u.RegDate }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserDetail(ParameterModel param)
        {
            var model = new UserModel();
            if (param.uidx != 0)
            {
                Mapper.CreateMap<USER, UserModel>();
                var userDto = userService.getUser(param);
                model = Mapper.Map<USER, UserModel>(userDto);
            }
            model.customerList = customerService.getCustomerList(new ParameterModel()).ToList();
            model.userTypeList = commonService.getCodeList(new ParameterModel { GROUPCD = 1006 }).ToList();
            return View(model);
        }

        public ActionResult updateUser(UserModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.CreateMap<UserModel, USER>();
                    USER entity = Mapper.Map<UserModel, USER>(model);
                    entity.RegId = userInfo.uidx;
                    result = userService.updateUser(entity);
                }
                else
                {
                    result.ResultCode = -1;
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

    }
}
