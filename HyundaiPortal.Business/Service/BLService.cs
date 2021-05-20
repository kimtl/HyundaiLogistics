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
    public class BLService : BaseService
    {
        
        public IQueryable<MBL> getMBLList(ParameterModel param)
        {
            var MBLList = HyundaiContext.MBL.Where(m=>m.midx != null);
            if (!string.IsNullOrEmpty(param.searchText))
            {
                if(param.searchKey == "mblNo")
                {
                    MBLList = MBLList.Where(c => c.mblNo.Contains(param.searchText));
                }
            }
            if (param.searchKey == "CreateDate")
            {
                MBLList = MBLList.Where(c => c.CreateDate >= param.startDate && c.CreateDate <= param.endDate);
            }
            else if (param.searchKey == "OnBoardDate")
            {
                MBLList = MBLList.Where(c => c.FltDate >= param.startDate && c.FltDate <= param.endDate);
            }
            return MBLList;
        }

        public MBL getMBL(ParameterModel param)
        {
            var mbl = HyundaiContext.MBL.Include("CUSTOMER").Include("OTHERCHARGE").Where(c => c.midx == param.midx).FirstOrDefault();
            return mbl;
        }

        public IList<OTHERCHARGE> getOtherChargeList(int midx)
        {
            var items = HyundaiContext.OTHERCHARGE.Where(p => p.midx == midx).ToList();
            return items;
        }

        public ResultModel updateMBL(MBL model)
        {
            ResultModel result = new ResultModel();
            
            try
            {
                if (model.midx == 0)
                {
                    model.CreateDate = DateTime.Now;
                    HyundaiContext.AddToMBL(model);
                }
                else
                {
                    MBL entity = HyundaiContext.MBL.Where(c => c.midx == model.midx).FirstOrDefault();
                    MBL newEntity = Mapper.Map(model, entity);
                    newEntity.CreateDate = entity.CreateDate;
                    newEntity.CreateId = entity.CreateId;
                    HyundaiContext.MBL.ApplyCurrentValues(newEntity);
                }

                HyundaiContext.SaveChanges();
                result.ResultMessage = model.midx.ToString();
            }
            catch(Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }
            return result;
        }

        public ResultModel deleteMBL(int midx)
        {
            ResultModel result = new ResultModel();

            try
            {
                var mblModel = HyundaiContext.MBL.Where(m => m.midx == midx).FirstOrDefault();
                if (mblModel.status != 31 && mblModel.status != 32)
                {
                    result.ResultCode = -2;
                }
                else
                {
                    var hblList = HyundaiContext.HBL.Where(h=>h.midx == midx);
                    deleteHBL(hblList.Select(h=>h.hidx).ToArray());
                    //delete othercharge
                    var otherlist = HyundaiContext.OTHERCHARGE.Where(o => o.midx == midx).ToList();
                    foreach (var item in otherlist)
                    {
                        HyundaiContext.OTHERCHARGE.DeleteObject(item);
                    }
                    HyundaiContext.MBL.DeleteObject(mblModel);

                    HyundaiContext.SaveChanges();
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

        public ResultModel updateMBLOtherCharge(int midx, IList<OTHERCHARGE> models)
        {
            ResultModel result = new ResultModel();

            try
            {
                foreach (var model in models)
                {
                    model.midx = midx;
                    if (model.ocidx == 0)
                    {
                        if(!string.IsNullOrEmpty(model.chargeCd)) {
                            HyundaiContext.AddToOTHERCHARGE(model);
                        }
                    }
                    else
                    {
                        OTHERCHARGE entity = HyundaiContext.OTHERCHARGE.Where(c => c.ocidx == model.ocidx).FirstOrDefault();
                        entity.chargeCd = model.chargeCd;
                        entity.ChargeAmt = model.ChargeAmt;
                        HyundaiContext.OTHERCHARGE.ApplyCurrentValues(entity);
                    }
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

        public IQueryable<HBL> getHBLList(ParameterModel param)
        {
            var HBLList = HyundaiContext.HBL.Include("MBL").Include("CODE").Include("PRODUCTITEM").Where(m => m.hidx != null);
            if (param.cidx != 0)
            {
                HBLList = HBLList.Where(h => h.cidx == param.cidx);
            }
            if (!string.IsNullOrEmpty(param.searchText))
            {
                if (param.searchKey == "mblNo")
                {
                    HBLList = HBLList.Where(c => c.MBL.mblNo.Contains(param.searchText));
                }
                else if (param.searchKey == "hblNo")
                {
                    HBLList = HBLList.Where(c => c.HblNo.Contains(param.searchText));
                }
                else if (param.searchKey == "midx")
                {
                    int midx = int.Parse(param.searchText);
                    HBLList = HBLList.Where(c => c.midx == midx);
                }
                else if (param.searchKey == "noMbl")
                {
                    HBLList = HBLList.Where(c => c.midx.Equals(null));
                }
            }
            if (param.status != 0)
            {
                HBLList = HBLList.Where(h => h.Status == param.status);
            }
            if (param.startDate != DateTime.MinValue && param.endDate != DateTime.MinValue)
            {
                HBLList = HBLList.Where(h => h.OnBoardDate >= param.startDate && h.OnBoardDate <= param.endDate);
            }
            
            return HBLList;
        }

        public HBL getHBL(ParameterModel param)
        {
            var hbl = HyundaiContext.HBL.Include("PRODUCTITEM").Where(c => c.hidx == param.hidx).FirstOrDefault();
            return hbl;
        }

        public IList<PRODUCTITEM> getProductItemList(int hidx)
        {
            var items = HyundaiContext.PRODUCTITEM.Where(p => p.hidx == hidx).ToList();
            return items;
        }

        public ResultModel updateHBL(HBL model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model.hidx == 0)
                {
                    result.ResultCode = -1;
                    result.ResultMessage = "Invalid parameter";
                    return result;
                }
                else
                {
                    HBL entity = HyundaiContext.HBL.Where(c => c.hidx == model.hidx).FirstOrDefault();
                    HBL newEntity = Mapper.Map(model, entity);

                    //ViewModel 에서 전달받지 않는 값들을 유지하기 위한 코드
                    newEntity.cidx = entity.cidx;
                    newEntity.CreateDate = entity.CreateDate;
                    newEntity.CreateId = entity.CreateId;
                    newEntity.midx = entity.midx;
                    
                    HyundaiContext.HBL.ApplyCurrentValues(newEntity);
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

        public ResultModel updateHBLConsoleList(int midx, int[] hidx)
        {
            ResultModel result = new ResultModel();

            try
            {
                var hblList = HyundaiContext.HBL.Where(c => c.midx == midx);
                foreach (var item in hblList)
                {
                    item.midx = null;
                    HyundaiContext.HBL.ApplyCurrentValues(item);
                }
                if (hidx != null)
                {
                    foreach (var item in hidx)
                    {
                        HBL entity = HyundaiContext.HBL.Where(c => c.hidx == item).FirstOrDefault();
                        entity.midx = midx;
                        HyundaiContext.HBL.ApplyCurrentValues(entity);
                    }
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

        public ResultModel updateHBLConsole(int hidx, int midx)
        {
            ResultModel result = new ResultModel();

            try
            {
                MBL mblModel = HyundaiContext.MBL.Where(c => c.midx == midx).FirstOrDefault();
                if (mblModel.status == 31)
                {
                    mblModel.status = 32;
                    HyundaiContext.MBL.ApplyCurrentValues(mblModel);
                }

                if (mblModel.status == 32)
                {
                    HBL entity = HyundaiContext.HBL.Where(c => c.hidx == hidx).FirstOrDefault();
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

        public ResultModel deleteHBL(int[] hidx)
        {
            ResultModel result = new ResultModel();

            try
            {
                foreach (var idx in hidx)
                {
                    var hbl = HyundaiContext.HBL.Include("PRODUCTITEM").Where(h => h.hidx == idx).FirstOrDefault();
                    if (hbl.Status == 20 || hbl.Status == 21 || hbl.Status == 22)
                    {
                        //Delete ProductItems
                        foreach (var productitem in hbl.PRODUCTITEM.ToList())
                        {
                            HyundaiContext.HBL.DeleteObject(hbl);
                        }
                        
                        //Delete HBL
                        HyundaiContext.DeleteObject(hbl);
                    }
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


        public ResultModel uploadHouseBL(USER userInfo, string filePath)
        {
            ResultModel result = new ResultModel();

            try
            {
                string e = Path.GetExtension(filePath).ToLower();
	            if (e != ".xls" && e != ".xlsx")
                {
                    result.ResultCode = -1;
                    result.ResultMessage = "엑셀 파일이 아닙니다.";
                }
                else
                {
                    var excelFIle = new ExcelQueryFactory(filePath);
                    var worksheetNames = excelFIle.GetWorksheetNames();
                    string sheetName="Sheet1";
                    foreach (var sheet in worksheetNames)
                    {
                        if (sheet.Substring(sheet.Length - 1) != "_")
                        {
                            var columnCnt = excelFIle.Worksheet(sheet).FirstOrDefault().Count();
                            if (columnCnt == 29 || columnCnt == 31 || columnCnt == 30 || columnCnt == 33 || columnCnt == 123)
                            {
                                sheetName = sheet;
                            }
                        }
                    }
                    var hblData = excelFIle.Worksheet(sheetName).ToList();
                    var converter = new KoreanRomanizer();

                    using (var scope = new TransactionScope(TransactionScopeOption.Required,
                                    TimeSpan.FromMinutes(5)))
                    {
                        int hidx = 0;
                        int hblCnt = 0;
                        string hblNo = "";
                        try
                        {
                            var transportCodeModel = HyundaiContext.CODE.Where(c=>c.GROUPCD == 1002).ToList();
                            
                            foreach (var item in hblData)
                            {
                                var model = new HBL();
                                if (item.Count() == 31)
                                {
                                    if (!string.IsNullOrEmpty(item[2]))
                                    {
                                        hblCnt++;
                                        model.HblNo = item[2].ToString().Trim();
                                        hblNo = item[2].ToString().Trim();
                                        if (hblNo == null)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "Invalid HBL No. : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }
                                        if (HyundaiContext.HBL.Where(h => h.HblNo == hblNo).Count() > 0)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "HBL already exists : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }

                                        if (item[4].Value != null && item[4].Value != "")
                                        {
                                            var dateStr = DateTime.Parse(item[4].Value.ToString().Split(' ')[0]);
                                            model.OnBoardDate = dateStr;
                                        }
                                        model.ShipperCd = item[5];
                                        model.ShipperName = item[6];
                                        model.ShipperAddress = item[7];
                                        model.ShipperCity = item[8];
                                        model.ShipperState = item[9];
                                        model.ShipperZipCode = item[10];
                                        model.ShipperPhone = item[11];
                                        model.ConsigneeName = item[12];
                                        model.ConsigneeEngName = converter.romanize(model.ConsigneeName);
                                        model.ConsigneeZipCode = item[13];
                                        model.ConsigneeZipAddress = item[14];
                                        model.ConsigneeAddress = item[15];
                                        model.ConsigneePhone = item[16];
                                        model.ConsigneeCellPhone = item[17];
                                        model.Memo = item[18];
                                        model.juminNo = item[19];
                                        string TransPortType = string.IsNullOrEmpty(item[21]) ? "B" : item[21];
                                        model.TransportType = transportCodeModel.Where(c => c.GROUPCD == 1002 && c.CD == TransPortType).FirstOrDefault().cdidx;
                                        model.WeightType = 5;
                                        model.Weight = Convert.ToDecimal(item[24]);
                                        model.Carton = Convert.ToInt16(item[25]);
                                        model.CreateDate = DateTime.Now;
                                        model.CreateId = userInfo.uidx;
                                        model.cidx = userInfo.cidx;
                                        model.Status = 20;
                                        string zipcode = model.ConsigneeZipCode.Replace("-", "");
                                        var zipModel = HyundaiContext.ZIPCODE.Where(c => c.Zipcode1 == zipcode).FirstOrDefault();
                                        if (zipModel != null)
                                        {
                                            model.EngZipaddress = zipModel.Address_Eng;
                                        }
                                        else
                                        {
                                            model.EngZipaddress = converter.romanize(model.ConsigneeZipAddress);
                                        }
                                        model.EngAddress = converter.romanize(model.ConsigneeAddress);
                                        HyundaiContext.AddToHBL(model);
                                        HyundaiContext.SaveChanges();
                                        hidx = model.hidx;

                                        //
                                        var productItem = getXLSProductItem(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                    else if (!string.IsNullOrEmpty(item[26]))
                                    {
                                        var productItem = getXLSProductItem(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                }
                                else if(item.Count() == 30)
                                {
                                    if (!string.IsNullOrEmpty(item[1]) && item[1] != "운송장번호")
                                    {
                                        hblCnt++;
                                        model.HblNo = item[1];
                                        hblNo = item[1];
                                        if (hblNo == null)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "Invalid HBL No. : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }
                                        if (HyundaiContext.HBL.Where(h => h.HblNo == hblNo).Count() > 0)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "HBL already exists : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }

                                        model.ShipperName = item[3];
                                        model.ShipperAddress = item[5] + " " + item[6];
                                        model.ShipperZipCode = item[4];
                                        model.ShipperPhone = item[7];
                                        model.ConsigneeName = item[9];
                                        model.ConsigneeEngName = converter.romanize(model.ConsigneeName);
                                        model.ConsigneeZipCode = item[11];
                                        model.ConsigneeZipAddress = item[12];
                                        model.ConsigneeAddress = item[13];
                                        model.ConsigneePhone = item[16];
                                        model.ConsigneeCellPhone = item[17];
                                        model.OnBoardDate = DateTime.Now;
                                        model.Memo = item[23];
                                        model.juminNo = item[8];
                                        string TransPortType = "B";
                                        model.TransportType = transportCodeModel.Where(c => c.GROUPCD == 1002 && c.CD == TransPortType).FirstOrDefault().cdidx;
                                        model.WeightType = 5;
                                        model.Weight = Convert.ToDecimal(item[19]);
                                        model.Carton = Convert.ToInt16(item[18]);
                                        model.CreateDate = DateTime.Now;
                                        model.CreateId = userInfo.uidx;
                                        model.cidx = userInfo.cidx;
                                        model.Status = 20;
                                        string zipcode = model.ConsigneeZipCode.Replace("-", "");
                                        var zipModel = HyundaiContext.ZIPCODE.Where(c => c.Zipcode1 == zipcode).FirstOrDefault();
                                        if (zipModel != null)
                                        {
                                            model.EngZipaddress = zipModel.Address_Eng;
                                        }
                                        else
                                        {
                                            model.EngZipaddress = converter.romanize(model.ConsigneeZipAddress);
                                        }
                                        model.EngAddress = converter.romanize(model.ConsigneeAddress);
                                        HyundaiContext.AddToHBL(model);
                                        HyundaiContext.SaveChanges();
                                        hidx = model.hidx;

                                        //
                                        var productItem = getXLSProductItem2(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                    else if (!string.IsNullOrEmpty(item[24]) && item[24] != "ITEM_NM")
                                    {
                                        var productItem = getXLSProductItem2(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                }
                                else if (item.Count() == 33) // 신 우정 양식
                                {
                                    if (!string.IsNullOrEmpty(item[1]) && item[1] != "운송장번호")
                                    {
                                        hblCnt++;
                                        model.HblNo = item[1];
                                        hblNo = item[1];
                                        if (hblNo == null)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "Invalid HBL No. : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }
                                        if (HyundaiContext.HBL.Where(h => h.HblNo == hblNo).Count() > 0)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "HBL already exists : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }

                                        model.ShipperName = item[3];
                                        model.ShipperAddress = item[5];
                                        model.ShipperZipAddress = item[6];
                                        model.ShipperZipCode = item[4];
                                        model.ShipperPhone = item[7];
                                        model.ConsigneeName = item[9];
                                        model.ConsigneeEngName = item[10];
                                        if (string.IsNullOrEmpty(model.ConsigneeEngName) || model.ConsigneeEngName == ".")
                                        {
                                            model.ConsigneeEngName = converter.romanize(model.ConsigneeName);
                                        }
                                        model.ConsigneeZipCode = item[11];
                                        model.ConsigneeZipAddress = item[12];
                                        model.ConsigneeAddress = item[13];
                                        model.EngZipaddress = item[14];
                                        model.EngAddress = item[15];
                                        if (string.IsNullOrEmpty(model.EngZipaddress) || model.EngZipaddress == ".")
                                        {
                                            string zipcode = model.ConsigneeZipCode.Replace("-", "");
                                            var zipModel = HyundaiContext.ZIPCODE.Where(c => c.Zipcode1 == zipcode).FirstOrDefault();
                                            if (zipModel != null)
                                            {
                                                model.EngZipaddress = zipModel.Address_Eng;
                                            }
                                            else
                                            {
                                                model.EngZipaddress = converter.romanize(model.ConsigneeZipAddress);
                                            }
                                        }
                                        if (string.IsNullOrEmpty(model.EngAddress) || model.EngAddress == ".")
                                        {
                                            model.EngAddress = converter.romanize(model.ConsigneeAddress);
                                        }
                                        model.ConsigneePhone = item[16];
                                        model.ConsigneeCellPhone = item[17];
                                        model.OnBoardDate = DateTime.Now;
                                        model.Memo = item[24];
                                        model.juminNo = item[8];
                                        string TransPortType = "B";
                                        model.TransportType = transportCodeModel.Where(c => c.GROUPCD == 1002 && c.CD == TransPortType).FirstOrDefault().cdidx;
                                        model.WeightType = 5;
                                        model.Weight = Convert.ToDecimal(item[19]);
                                        model.Carton = Convert.ToInt16(item[18]);
                                        model.CreateDate = DateTime.Now;
                                        model.CreateId = userInfo.uidx;
                                        model.cidx = userInfo.cidx;
                                        model.Status = 20;
                                        HyundaiContext.AddToHBL(model);
                                        HyundaiContext.SaveChanges();
                                        hidx = model.hidx;

                                        //
                                        var productItem = getXLSProductItem3(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                    else if (!string.IsNullOrEmpty(item[29]) && item[29] != "ITEM_NM")
                                    {
                                        var productItem = getXLSProductItem3(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                }
                                else if (item.Count() == 29) // SHIN
                                {
                                    if (!string.IsNullOrEmpty(item[0]) && (item[0] != "SEQ_NO" && item[0] != "순번(NO)"))
                                    {
                                        hblCnt++;
                                        model.HblNo = item[1];
                                        hblNo = item[1];
                                        if (hblNo == null)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "Invalid HBL No. : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }
                                        if (HyundaiContext.HBL.Where(h => h.HblNo == hblNo).Count() > 0)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "HBL already exists : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }

                                        model.ShipperName = item[3];
                                        model.ShipperAddress = item[5];
                                        model.ShipperZipAddress = item[6];
                                        model.ShipperZipCode = item[4];
                                        model.ShipperPhone = item[7];
                                        model.ConsigneeName = item[9];
                                        model.ConsigneeEngName = item[10];
                                        if(string.IsNullOrEmpty(model.ConsigneeEngName) || model.ConsigneeEngName == ".")
                                        {
                                            model.ConsigneeEngName = converter.romanize(model.ConsigneeName);
                                        }
                                        model.ConsigneeZipCode = item[11];
                                        model.ConsigneeZipAddress = item[12];
                                        model.ConsigneeAddress = item[13];
                                        model.EngZipaddress = item[14];
                                        model.EngAddress = item[15];
                                        if(string.IsNullOrEmpty(model.EngZipaddress) || model.EngZipaddress == ".")
                                        {
                                            model.EngZipaddress = converter.romanize(model.ConsigneeZipAddress);
                                        }
                                        if (string.IsNullOrEmpty(model.EngAddress) || model.EngAddress == ".")
                                        {
                                            model.EngAddress = converter.romanize(model.ConsigneeAddress);
                                        }
                                        model.ConsigneePhone = item[16];
                                        model.ConsigneeCellPhone = item[17];
                                        model.OnBoardDate = DateTime.Now;
                                        model.Memo = item[23];
                                        model.juminNo = item[8];
                                        string TransPortType = "B";
                                        model.TransportType = transportCodeModel.Where(c => c.GROUPCD == 1002 && c.CD == TransPortType).FirstOrDefault().cdidx;
                                        model.WeightType = 5;
                                        model.Weight = Convert.ToDecimal(item[19]);
                                        model.Carton = Convert.ToInt16(item[18]);
                                        model.CreateDate = DateTime.Now;
                                        model.CreateId = userInfo.uidx;
                                        model.cidx = userInfo.cidx;
                                        model.Status = 20;
                                        HyundaiContext.AddToHBL(model);
                                        HyundaiContext.SaveChanges();
                                        hidx = model.hidx;

                                        //
                                        var productItem = getXLSProductItem2(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                    else if (!string.IsNullOrEmpty(item[25]) && item[25] != "ITEM_NM")
                                    {
                                        var productItem = getXLSProductItem2(item, hidx);
                                        HyundaiContext.AddToPRODUCTITEM(productItem);
                                    }
                                }
                                else if (item.Count() == 123) // EZ
                                {
                                    if (!string.IsNullOrEmpty(item[1]) && item[1] != "주문번호")
                                    {
                                        hblCnt++;
                                        model.HblNo = item[2];
                                        hblNo = item[2];
                                        if (hblNo == null)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "Invalid HBL No. : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }
                                        if (HyundaiContext.HBL.Where(h => h.HblNo == hblNo).Count() > 0)
                                        {
                                            result.ResultCode = -1;
                                            result.ResultMessage = "HBL already exists : " + hblNo;
                                            scope.Dispose();
                                            return result;
                                        }

                                        model.ShipperName = item[6];
                                        model.ShipperZipAddress = item[7] ?? "";
                                        model.ShipperZipCode = model.ShipperZipAddress.Length > 5 ? model.ShipperZipAddress.Substring(model.ShipperZipAddress.Length - 5) : "";
                                        model.ShipperZipAddress = model.ShipperZipAddress.Replace(model.ShipperZipCode, "");
                                        model.ConsigneeName = item[10];
                                        model.ConsigneeEngName = converter.romanize(model.ConsigneeName);
                                        model.juminNo = item[11];
                                        model.ConsigneePhone = item[12];
                                        model.ConsigneeCellPhone = item[13];
                                        model.ConsigneeZipCode = item[14];
                                        model.ConsigneeZipAddress = item[15];
                                        model.ConsigneeAddress = item[16];
                                        model.EngZipaddress = converter.romanize(model.ConsigneeZipAddress);
                                        model.EngAddress = converter.romanize(model.ConsigneeAddress);
                                        model.OnBoardDate = DateTime.Now;
                                        model.Memo = item[22];
                                        string TransPortType = "B";
                                        model.TransportType = transportCodeModel.Where(c => c.GROUPCD == 1002 && c.CD == TransPortType).FirstOrDefault().cdidx;
                                        model.WeightType = 5;
                                        model.Weight = Convert.ToDecimal(item[18]);
                                        model.Carton = Convert.ToInt16(item[20]);
                                        model.CreateDate = DateTime.Now;
                                        model.CreateId = userInfo.uidx;
                                        model.cidx = userInfo.cidx;
                                        model.Status = 20;
                                        HyundaiContext.AddToHBL(model);
                                        HyundaiContext.SaveChanges();
                                        hidx = model.hidx;

                                        //
                                        for(int i=23;i<123;i+=4)
                                        {
                                            if(string.IsNullOrEmpty(item[i]))
                                            {
                                                break;
                                            }
                                            PRODUCTITEM itemModel = new PRODUCTITEM();
                                            itemModel.itemName = item[i];
                                            itemModel.itemAmt = Convert.ToDecimal(item[i+1]);
                                            itemModel.itemQty = Convert.ToInt16(item[i+2]);
                                            itemModel.ItemTotalAmt = Convert.ToDecimal(item[i+3]);
                                            itemModel.hidx = hidx;
                                            HyundaiContext.AddToPRODUCTITEM(itemModel);
                                        }
                                    }
                                }
                                HyundaiContext.SaveChanges();
                            }

                        }
                        catch (Exception ex)
                        {
                            File.Delete(filePath);
                            throw new Exception("Error on HBL NO : " + hblNo, ex);
                        }
                        scope.Complete();
                        result.ResultMessage = hblCnt.ToString();
                    }
                }
                File.Delete(filePath);
                
            }
            catch (Exception ex)
            {
                result.ResultCode = -1;
                result.ResultMessage = ex.Message;
                throw ex;
            }
            return result;
        }

        private PRODUCTITEM getXLSProductItem(IList<Cell> sheetData, int hidx)
        {
            var model = new PRODUCTITEM();
            model.hidx = hidx;
            model.itemName = sheetData[26];
            model.url = sheetData[27];
            model.itemQty = Convert.ToInt16(sheetData[28]);
            model.itemAmt = Convert.ToDecimal(sheetData[29]);
            model.ItemTotalAmt = Convert.ToDecimal(sheetData[30]);
            return model;
        }

        private PRODUCTITEM getXLSProductItem2(IList<Cell> sheetData, int hidx)
        {
            var model = new PRODUCTITEM();
            model.hidx = hidx;
            model.itemName = sheetData[25];
            model.itemQty = Convert.ToInt16(sheetData[26]);
            model.itemAmt = Convert.ToDecimal(sheetData[27]);
            model.ItemTotalAmt = Convert.ToDecimal(sheetData[28]);
            return model;
        }

        private PRODUCTITEM getXLSProductItem3(IList<Cell> sheetData, int hidx)
        {
            var model = new PRODUCTITEM();
            model.hidx = hidx;
            model.itemName = sheetData[28];
            model.itemQty = Convert.ToInt16(sheetData[29]);
            model.itemAmt = Convert.ToDecimal(sheetData[30]);
            model.ItemTotalAmt = Convert.ToDecimal(sheetData[31]);
            return model;
        }

        public PRODUCTITEM getProductItem(ParameterModel param)
        {
            var model = HyundaiContext.PRODUCTITEM.Where(c => c.pidx == param.pidx).FirstOrDefault();
            return model;
        }

        public ResultModel updateHouseItem(PRODUCTITEM model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model.pidx == 0)
                {
                    HyundaiContext.AddToPRODUCTITEM(model);
                }
                else
                {
                    PRODUCTITEM entity = HyundaiContext.PRODUCTITEM.Where(c => c.pidx == model.pidx).FirstOrDefault();
                    PRODUCTITEM newEntity = Mapper.Map(model, entity);
                    newEntity.hidx = entity.hidx;
                    HyundaiContext.PRODUCTITEM.ApplyCurrentValues(newEntity);
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

        public ResultModel deleteHouseItem(ParameterModel model)
        {
            ResultModel result = new ResultModel();

            try
            {
                if (model.pidx != 0)
                {
                    PRODUCTITEM entity = HyundaiContext.PRODUCTITEM.Where(c => c.pidx == model.pidx).FirstOrDefault();
                    HyundaiContext.PRODUCTITEM.DeleteObject(entity);
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
