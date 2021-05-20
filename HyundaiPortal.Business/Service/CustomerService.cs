using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HyundaiPortal.Business.Model;
using AutoMapper;

namespace HyundaiPortal.Business.Service
{
    public class CustomerService : BaseService
    {
        
        public IQueryable<CUSTOMER> getCustomerList(ParameterModel param)
        {
            var customerList = HyundaiContext.CUSTOMER.Where(c => c.isDelete == false);
            if (!string.IsNullOrEmpty(param.searchText))
            {
                if(param.searchKey == "CustName")
                {
                    customerList = customerList.Where(c => c.CustName.Contains(param.searchText));
                }
                else if (param.searchKey == "OnwerName")
                {
                    customerList = customerList.Where(c => c.OwnerName.Contains(param.searchText));
                }
            }
            return customerList;
        }

        public CUSTOMER getCustomer(ParameterModel param)
        {
            var customer = HyundaiContext.CUSTOMER.Where(c => c.cidx == param.cidx).FirstOrDefault();
            return customer;
        }

        public ResultModel updateCustomer(CUSTOMER model)
        {
            ResultModel result = new ResultModel();
            
            try
            {
                if (model.cidx == 0)
                {
                    HyundaiContext.AddToCUSTOMER(model);
                }
                else
                {
                    CUSTOMER entity = HyundaiContext.CUSTOMER.Where(c=>c.cidx == model.cidx).FirstOrDefault();
                    CUSTOMER newEntity = Mapper.Map(model, entity);
                    HyundaiContext.CUSTOMER.ApplyCurrentValues(newEntity);
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

        public IQueryable<RATE> getRateList(ParameterModel param)
        {
            var rateList = HyundaiContext.RATE.Where(c => c.cidx == param.cidx).OrderByDescending(c=>c.ridx);
            return rateList;
        }

        public RATE getRate(ParameterModel param)
        {
            var rate = HyundaiContext.RATE.Where(c => c.ridx == param.ridx).FirstOrDefault();
            return rate;
        }

        public ResultModel updateRate(RATE model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model.ridx == 0)
                {
                    HyundaiContext.AddToRATE(model);
                }
                else
                {
                    RATE entity = HyundaiContext.RATE.Where(c => c.ridx == model.ridx).FirstOrDefault();
                    RATE newEntity = Mapper.Map(model, entity);
                    newEntity.RegId = entity.RegId;
                    HyundaiContext.RATE.ApplyCurrentValues(newEntity);
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
