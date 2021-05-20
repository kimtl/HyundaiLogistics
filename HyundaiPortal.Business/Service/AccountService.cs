using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;

namespace HyundaiPortal.Business.Service
{
    public class AccountService : BaseService
    {
        
        public bool getUserValid(LoginModel model)
        {
            var userModel = HyundaiContext.USER.Where(u => u.userid == model.UserName && u.password == model.Password).FirstOrDefault();
            return userModel != null;
        }

        public USER getUserInfo(string UserName)
        {
            var userModel = HyundaiContext.USER.Where(u => u.userid == UserName).FirstOrDefault();
            return userModel;
        }
    }
}
