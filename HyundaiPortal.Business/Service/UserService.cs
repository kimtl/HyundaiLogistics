using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;
using AutoMapper;

namespace HyundaiPortal.Business.Service
{
    public class UserService : BaseService
    {
        
        public IQueryable<USER> getUserList(ParameterModel param)
        {
            var userList = HyundaiContext.USER.Where(c => c.isDelete == false);
            if (!string.IsNullOrEmpty(param.searchText))
            {
                if(param.searchKey == "UserName")
                {
                    userList = userList.Where(c => c.UserName.Contains(param.searchText));
                }
                else if (param.searchKey == "UserID")
                {
                    userList = userList.Where(c => c.userid.Contains(param.searchText));
                }
            }
            return userList;
        }

        public USER getUser(ParameterModel param)
        {
            var user = HyundaiContext.USER.Where(c => c.uidx == param.uidx).FirstOrDefault();
            return user;
        }

        public ResultModel updateUser(USER model)
        {
            ResultModel result = new ResultModel();
            
            try
            {
                if (model.uidx == 0)
                {
                    model.RegDate = DateTime.Now;
                    HyundaiContext.AddToUSER(model);
                }
                else
                {
                    USER entity = HyundaiContext.USER.Where(c => c.uidx == model.uidx).FirstOrDefault();
                    entity.UserName = model.UserName;
                    entity.userid = model.userid;
                    entity.cidx = model.cidx;
                    entity.UserType = model.UserType;
                    entity.UserEmail = model.UserEmail;
                    HyundaiContext.USER.ApplyCurrentValues(entity);
                }

                HyundaiContext.SaveChanges();
            }
            catch(Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }
            return result;
        }

    }
}
