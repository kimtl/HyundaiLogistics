using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;
using AutoMapper;
using System.Data.Entity.SqlServer;

namespace HyundaiPortal.Business.Service
{
    public class ItemService : BaseService
    {
        public ResultModel updateHBLConsole(string hblNo, int midx)
        {
            ResultModel result = new ResultModel();

            try
            {
                //Load BL models
                MBL mblModel = HyundaiContext.MBL.Where(c => c.midx == midx).FirstOrDefault();
                HBL entity = HyundaiContext.HBL.Where(c => c.HblNo == hblNo).FirstOrDefault();
                
                //Check existing console
                if (entity == null)
                {
                    result.ResultCode = -2;
                    result.ResultMessage = "HBL not found";
                    return result;
                }
                else if (entity.Status == 22)
                {
                    result.ResultCode = -3;
                    result.ResultMessage = "This HBL has already been processed";
                    return result;
                }

                if (mblModel.status == 31)
                {
                    mblModel.status = 32;
                    HyundaiContext.MBL.ApplyCurrentValues(mblModel);
                }

                if (mblModel.status == 32)
                {
                    entity.midx = midx;
                    entity.Status = 22;
                    HyundaiContext.HBL.ApplyCurrentValues(entity);
                    HyundaiContext.SaveChanges();
                }
                else
                {
                    result.ResultCode = -2;
                }

            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }
            return result;
        }

    }
}
