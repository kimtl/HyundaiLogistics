using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;
using AutoMapper;
using System.Data.Entity.SqlServer;
using LinqToExcel;
using System.IO;
using System.Data.Entity;
using System.Transactions;
using HyundaiPortal.Business.Util;

namespace HyundaiPortal.Business.Service
{
    public class ConsoleService : BaseService
    {
        public ResultModel setRelease(int midx)
        {
            ResultModel result = new ResultModel();

            try
            {
                var mbl = HyundaiContext.MBL.Where(m => m.midx == midx).FirstOrDefault();
                mbl.status = 33;
                HyundaiContext.MBL.ApplyCurrentValues(mbl);
                var hblList = HyundaiContext.HBL.Where(m => m.midx == midx).ToList();
                foreach(var item in hblList)
                {
                    item.Status = 23;
                    HyundaiContext.HBL.ApplyCurrentValues(item);
                }
                HyundaiContext.SaveChanges();

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
