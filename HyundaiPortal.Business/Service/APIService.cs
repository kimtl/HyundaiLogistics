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
    public class APIService : BaseService
    {

        public ResultModel checkAPIKey(int cidx, string apikey)
        {
            var result = new ResultModel();
            if (HyundaiContext.CUSTOMER.Where(c => c.cidx == cidx && c.ApiKey == apikey).Count() != 1)
            {
                result.ResultCode = -1;
                result.ResultMessage = "API Key Not Matched";
            }
            return result;
        }

        public ResultModel setHBL(HBLAPIModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                var hbl = new HBL();
                Mapper.CreateMap<HBLAPIModel,HBL>();
                Mapper.Map(model,hbl);
                
                    var converter = new KoreanRomanizer();

                    using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    TimeSpan.FromMinutes(5)))
                    {
                        int hidx = 0;
                        int hblCnt = 0;
                        string hblNo = "";
                        var transportCodeModel = HyundaiContext.CODE.Where(c => c.GROUPCD == 1002).ToList();

                        if (!string.IsNullOrEmpty(hbl.HblNo))
                        {
                            if (hbl.HblNo == null || hbl.HblNo.Length != 13)
                            {
                                result.ResultCode = -1;
                                result.ResultMessage = "Invalid HBL No. : " + hblNo;
                                scope.Dispose();
                                return result;
                            }
                            if (HyundaiContext.HBL.Where(h => h.HblNo == hbl.HblNo).Count() > 0)
                            {
                                result.ResultCode = -1;
                                result.ResultMessage = "HBL already exists : " + hblNo;
                                scope.Dispose();
                                return result;
                            }
                            if (string.IsNullOrEmpty(hbl.ConsigneeEngName))
                            {
                                hbl.ConsigneeEngName = converter.romanize(hbl.ConsigneeName);
                            }
                            if (string.IsNullOrEmpty(hbl.EngZipaddress))
                            {
                                string zipcode = hbl.ConsigneeZipCode.Replace("-", "");
                                var zipModel = HyundaiContext.ZIPCODE.Where(c => c.Zipcode1 == zipcode).FirstOrDefault();
                                if (zipModel != null)
                                {
                                    hbl.EngZipaddress = zipModel.Address_Eng;
                                }
                                else
                                {
                                    hbl.EngZipaddress = converter.romanize(hbl.ConsigneeZipAddress);
                                }
                            }
                            if (string.IsNullOrEmpty(hbl.EngAddress))
                            {
                                hbl.EngAddress = converter.romanize(hbl.ConsigneeAddress);
                            }
                            hbl.WeightType = 5;
                            hbl.CreateDate = DateTime.Now;
                            hbl.Status = 20;


                            HyundaiContext.AddToHBL(hbl);
                            HyundaiContext.SaveChanges();
                            hidx = hbl.hidx;

                            //priductitem
                            foreach (var item in model.itemList)
                            {
                                var productItem = new PRODUCTITEM();
                                productItem.hidx = hidx;
                                productItem.itemAmt = item.itemAmt;
                                productItem.itemBrand = item.itemBrand;
                                productItem.itemName = item.itemName;
                                productItem.itemQty = item.itemQty;
                                productItem.ItemTotalAmt = item.ItemTotalAmt;
                                productItem.url = item.url;
                                HyundaiContext.AddToPRODUCTITEM(productItem);
                            }
                            HyundaiContext.SaveChanges();
                        }
                        else
                        {
                            result.ResultCode = -1;
                            result.ResultMessage = "Invalid HBL No.";
                        }
                        
                    scope.Complete();
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
