using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;
using AutoMapper;

namespace HyundaiPortal.Business.Service
{
    public class InvoiceService : BaseService
    {
        
        public IQueryable<MBL> getCustomerList(ParameterModel param)
        {
            var customerList = HyundaiContext.MBL.Where(m => m.FltDate >= param.startDate && m.FltDate <= param.endDate);
            return customerList;
        }

        public decimal getRate(int cidx, DateTime date)
        {
            var rate = HyundaiContext.RATE.Where(r => r.cidx == cidx && r.applyDate <= date).OrderByDescending(r => r.applyDate).First();
            return rate.baseRate ?? 0;
        }
    }
}
