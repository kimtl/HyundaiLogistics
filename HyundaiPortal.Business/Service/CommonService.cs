using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;
using AutoMapper;

namespace HyundaiPortal.Business.Service
{
    public class CommonService : BaseService
    {
        
        public IQueryable<CODE> getCodeList(ParameterModel param)
        {
            var codeList = HyundaiContext.CODE.Where(c => c.GROUPCD == param.GROUPCD && c.CD != "").OrderBy(c=>c.SORT);
            return codeList;
        }
    }
}
